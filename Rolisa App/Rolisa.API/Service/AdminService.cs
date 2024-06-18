using Microsoft.EntityFrameworkCore.Storage;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Service
{
    public class AdminService
    {
        private VMResponse response = new VMResponse();
        private RolisaContext db;
        public AdminService(RolisaContext _db) { db = _db; }

        public VMResponse GetAll()
        {
            try
            {
                List<VMAdmin> data = (
                    from a in db.Admins
                    join u in db.Users on a.UserId equals u.Id
                    join b in db.Biodata on u.BiodataId equals b.Id
                    where a.IsDeleted == false
                    select new VMAdmin
                    {
                        Id = a.Id,
                        UserId = a.UserId,
                        Code = a.Code,
                        CreatedBy = a.CreatedBy,
                        CreatedOn = a.CreatedOn,
                        ModifiedBy = a.ModifiedBy,
                        ModifiedOn = a.ModifiedOn,
                        DeletedBy = a.DeletedBy,
                        DeletedOn = a.DeletedOn,
                        IsDeleted = a.IsDeleted,
                    }
                ).ToList();
                if (data != null)
                {
                    response.statusCode = (data.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
                    response.message = (data.Count > 0) ? $"success fetched {data.Count} data admin" : "admin has no data!";
                    response.data = data;
                }
                else
                {
                    throw new ArgumentNullException("no data");
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return response;
        }
        public VMResponse GetById(int id)
        {
            try
            {
                VMAdmin? admin = null;
                VMBiodatum? biodatum = null;
                VMUser? user = null;

                if (id != 0)
                {
                    admin = (
                        from a in db.Admins
                        join u in db.Users on a.UserId equals u.Id
                        join b in db.Biodata on u.BiodataId equals b.Id
                        where a.IsDeleted == false && a.Id == id
                        select new VMAdmin
                        {
                            Id = a.Id,
                            Code = a.Code,
                            UserId = a.UserId,
                            CreatedBy = a.CreatedBy,
                            CreatedOn = a.CreatedOn,
                            ModifiedBy = a.ModifiedBy,
                            ModifiedOn = a.ModifiedOn,
                            DeletedBy = a.DeletedBy,
                            DeletedOn = a.DeletedOn,
                            IsDeleted = a.IsDeleted,
                        }
                    ).FirstOrDefault();
                    if (admin != null)
                    {
                        user = (
                            from u in db.Users
                            where u.Id == admin.UserId && u.IsDeleted == false
                            select new VMUser
                            {
                                Id = u.Id,
                                BiodataId = u.BiodataId,
                                RoleId = u.RoleId,
                                Email = u.Email,
                                Password = u.Password,
                                IsBlocked = u.IsBlocked,
                                UnblockedOn = u.UnblockedOn,
                                LoginAttempt = u.LoginAttempt,
                                LastLogin = u.LastLogin,
                                CreatedBy = u.CreatedBy,
                                CreatedOn = u.CreatedOn,
                                ModifiedBy = u.ModifiedBy,
                                ModifiedOn = u.ModifiedOn,
                                DeletedBy = u.DeletedBy,
                                DeletedOn = u.DeletedOn,
                                IsDeleted = u.IsDeleted,
                            }
                        ).FirstOrDefault();

                        biodatum = (
                            from b in db.Biodata
                            where b.Id == user.BiodataId && b.IsDeleted == false
                            select new VMBiodatum
                            {
                                Id = b.Id,
                                Nickname = b.Nickname,
                                Fullname = b.Fullname,
                                Phone = b.Phone,
                                ImagePath = b.ImagePath,
                                CreatedBy = b.CreatedBy,
                                CreatedOn = b.CreatedOn,
                                ModifiedBy = b.ModifiedBy,
                                ModifiedOn = b.ModifiedOn,
                                DeletedBy = b.DeletedBy,
                                DeletedOn = b.DeletedOn,
                                IsDeleted = b.IsDeleted,
                            }
                        ).FirstOrDefault();

                        admin.biodatum = biodatum;
                        admin.user = user;
                    }
                }
                else
                {
                    throw new Exception("please input id first!");
                }
                if (admin != null)
                {
                    response.message = "success get data by id";
                    response.statusCode = System.Net.HttpStatusCode.OK;
                    response.data = admin;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return response;
        }
        public VMResponse Create(VMAdmin data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    if (data != null)
                    {
                        Admin admin = new Admin();
                        User user = new User();
                        Biodatum biodatum = new Biodatum();

                        biodatum.Fullname = data.biodatum.Fullname;
                        biodatum.Nickname = data.biodatum.Nickname;
                        biodatum.Phone = data.biodatum.Phone;
                        biodatum.CreatedBy = data.CreatedBy;
                        db.Add(biodatum);
                        db.SaveChanges();

                        user.Email = data.user.Email;
                        user.Password = BCrypt.Net.BCrypt.HashPassword(data.user.Password);
                        user.RoleId = 1; //admin role id
                        user.BiodataId = biodatum.Id;
                        user.IsBlocked = false;
                        user.CreatedBy = data.CreatedBy;
                        db.Add(user);
                        db.SaveChanges();

                        admin.UserId = user.Id;
                        admin.Code = $"ADM-RLS-{biodatum.Id}";
                        admin.CreatedBy = data.CreatedBy;
                        db.Add(admin);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("failed to create admin data!");
                    }
                    dbTran.Commit();
                    response.message = "success create admin data";
                    response.statusCode = System.Net.HttpStatusCode.Created;
                    response.data = data;
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    response.message = ex.Message;
                    response.statusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            return response;
        }
        public VMResponse Update(VMAdmin data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    Admin admin = new Admin();
                    User user = new User();
                    Biodatum biodatum = new Biodatum();

                    VMResponse response = GetById(data.Id);
                    object? dataResponse = response.data;
                    VMAdmin existingData = (VMAdmin)dataResponse;

                    if (existingData != null)
                    {
                        biodatum.Id = existingData.biodatum.Id;
                        biodatum.Fullname = data.biodatum.Fullname;
                        biodatum.Nickname = data.biodatum.Nickname;
                        biodatum.Phone = data.biodatum.Phone;
                        biodatum.ImagePath = data.biodatum.ImagePath;
                        biodatum.CreatedBy = existingData.biodatum.CreatedBy ?? 0;
                        biodatum.CreatedOn = existingData.biodatum.CreatedOn ?? DateTime.Now;
                        biodatum.ModifiedBy = data.ModifiedBy;
                        biodatum.ModifiedOn = DateTime.Now;
                        biodatum.IsDeleted = false;
                        db.Update(biodatum);
                        db.SaveChanges();

                    }
                    else
                    {
                        throw new Exception("data admin cannot found");
                    }
                    response.message = "success edit admin data";
                    response.statusCode = System.Net.HttpStatusCode.OK;
                    response.data = data;
                    dbTran.Commit();
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    response.message = ex.Message;
                    response.statusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            return response;
        }
    }
}
