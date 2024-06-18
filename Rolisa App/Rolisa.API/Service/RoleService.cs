using Microsoft.EntityFrameworkCore.Storage;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Service
{
    public class RoleService
    {
        private VMResponse response = new VMResponse();
        private readonly RolisaContext db;
        public RoleService(RolisaContext _db)
        {
            db = _db;
        }
        public VMResponse GetByFilter(string filter)
        {
            try
            {
                List<VMRole>? role = (
                    from r in db.Roles
                    where r.IsDeleted == false && r.Name.Contains(filter ?? "")
                    select new VMRole
                    {
                        Id = r.Id,
                        Name = r.Name,
                        CreatedBy = r.CreatedBy,
                        CreatedOn = r.CreatedOn,
                        ModifiedBy = r.ModifiedBy,
                        ModifiedOn = r.ModifiedOn,
                        DeletedBy = r.DeletedBy,
                        DeletedOn = r.DeletedOn,
                        IsDeleted = r.IsDeleted
                    }
                ).ToList();
                if (filter != null)
                {
                    response.statusCode = (role.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
                    response.message = (role.Count > 0) ? $"success fetched {role.Count} data product category" : "product category has no data!";
                    response.data = role;
                }
                else
                {
                    response.statusCode = System.Net.HttpStatusCode.NoContent;
                    response.message = "please input filter first!";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return response;
        }
        public VMResponse GetAll() => GetByFilter("");
        public VMResponse GetById(int id)
        {
            try
            {
                VMRole? role = (
                    from r in db.Roles
                    where r.IsDeleted == false && r.Id == id
                    select new VMRole
                    {
                        Id = r.Id,
                        Name = r.Name,
                        CreatedBy = r.CreatedBy,
                        CreatedOn = r.CreatedOn,
                        ModifiedBy = r.ModifiedBy,
                        ModifiedOn = r.ModifiedOn,
                        DeletedBy = r.DeletedBy,
                        DeletedOn = r.DeletedOn,
                        IsDeleted = r.IsDeleted
                    }
                ).FirstOrDefault();

                if (role != null)
                {
                    response.statusCode = System.Net.HttpStatusCode.OK;
                    response.message = "success get data by id" + id;
                    response.data = role;
                }
                else
                {
                    response.statusCode = System.Net.HttpStatusCode.NoContent;
                    response.message = $"data with id {id} not found";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return response;
        }
        public VMResponse Create(VMRole data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    Role role = new Role()
                    {
                        Name = data.Name,
                        CreatedBy = data.CreatedBy,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false
                    };
                    db.Add(role);
                    db.SaveChanges();
                    dbTran.Commit();

                    response.statusCode = System.Net.HttpStatusCode.Created;
                    response.message = "success create data role";
                    response.data = role;
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
        public VMResponse Update(VMRole data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                VMResponse response = GetById(data.Id);
                object? dataResponse = response.data;
                VMRole existingData = (VMRole)dataResponse;
                try
                {
                    if (existingData != null)
                    {
                        Role role = new Role()
                        {
                            Id = existingData.Id,
                            Name = data.Name,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        db.Update(role);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = "success update data inventory category";
                        response.data = role;
                    }
                    else
                    {
                        response.statusCode = System.Net.HttpStatusCode.NoContent;
                        response.message = "cannot update data inventory category, data not found";
                    }

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
        public VMResponse Delete(int id, int userId)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                VMResponse response = GetById(id);
                object? dataResponse = response.data;
                VMRole existingData = (VMRole)dataResponse;
                try
                {
                    if (existingData != null)
                    {
                        Role role = new Role()
                        {
                            Id = existingData.Id,
                            Name = existingData.Name,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = existingData.ModifiedBy,
                            ModifiedOn = existingData.ModifiedOn,
                            DeletedBy = userId,
                            DeletedOn = DateTime.Now,
                            IsDeleted = true
                        };
                        db.Update(role);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = "success delete data role";
                        response.data = role;
                    }
                    else
                    {
                        response.statusCode = System.Net.HttpStatusCode.NoContent;
                        response.message = "cannot delete data role, data not found";
                    }
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
