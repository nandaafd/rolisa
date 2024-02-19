using Microsoft.EntityFrameworkCore.Storage;
using Rolisa.DataModel;
using Rolisa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rolisa.DataAccess
{
    public class DAUser
    {
        private VMResponse response = new VMResponse();
        private readonly RolisaContext db;
        public DAUser(RolisaContext _db) { db = _db; }
        public VMResponse GetById(int id)
        {
            try
            {
                if (id != null)
                {
                    VMUser? user = (
                        from u in db.Users
                        where u.Id == id && u.IsBlocked == false && u.IsDeleted == false
                        select new VMUser
                        {
                            Id = u.Id,
                            BiodataId = u.BiodataId,
                            RoleId = u.RoleId,
                            Email = u.Email,
                            Password = u.Password,
                            LoginAttempt = u.LoginAttempt,
                            IsBlocked = u.IsBlocked,
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
                    if (user != null)
                    {
                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = "success get data by id";
                        response.data = user;
                    }
                    else
                    {
                        throw new Exception("data cannot found");
                    }
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch(Exception ex) {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return response;
        }
        public List<VMResetPassword> GetByEmail(string email)
        {
            List<VMResetPassword>? resetpass = new List<VMResetPassword>();
            try
            {

                if (email != null || email != "")
                {
                    resetpass = (
                    from rp in db.ResetPasswords
                    where rp.Email == email && rp.IsDeleted == false
                    select new VMResetPassword
                    {
                        Id = rp.Id,
                        Email = rp.Email,
                        OldPassword = rp.OldPassword,
                        NewPassword = rp.NewPassword,
                        CreatedBy = rp.CreatedBy,
                        CreatedOn = rp.CreatedOn,
                        ModifiedBy = rp.ModifiedBy,
                        ModifiedOn = rp.ModifiedOn,
                        DeletedBy = rp.DeletedBy,
                        DeletedOn = rp.DeletedOn,
                        IsDeleted = rp.IsDeleted,
                    }
                    ).ToList();
                    if (resetpass != null)
                    {
                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = "success get data by id";
                        response.data = resetpass;
                    }
                    else
                    {
                        throw new Exception("data not found");
                    }
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch(Exception ex )
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return resetpass;
        }
        public VMResponse ChangePassword(VMUser data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMResponse response = GetById(data.Id);
                    object? dataResponse = response.data;
                    VMUser? existingData = (VMUser)dataResponse;

                    List<VMResetPassword> passwordHistory = GetByEmail(data.Email);

                    if (existingData != null)
                    {
                        if (BCrypt.Net.BCrypt.Verify(data.OldPassword, existingData.Password))
                        {
                            if (!passwordHistory.Any(p => BCrypt.Net.BCrypt.Verify(data.Password, p.OldPassword)) && data.OldPassword != data.Password)
                            {
                                User user = new User();
                                user.Id = existingData.Id;
                                user.RoleId = existingData.RoleId;
                                user.BiodataId = existingData.BiodataId;
                                user.Email = existingData.Email;
                                user.LoginAttempt = existingData.LoginAttempt;
                                user.IsBlocked = existingData.IsBlocked;
                                user.LastLogin = existingData.LastLogin;
                                user.CreatedBy = existingData.CreatedBy ?? 0;
                                user.CreatedOn = existingData.CreatedOn ?? DateTime.Now;
                                user.ModifiedBy = data.ModifiedBy;
                                user.ModifiedOn = DateTime.Now;
                                user.IsDeleted = false;

                                user.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);

                                db.Update(user);
                                db.SaveChanges();

                                ResetPassword reset = new ResetPassword();
                                reset.Email = user.Email;
                                reset.OldPassword = BCrypt.Net.BCrypt.HashPassword(data.OldPassword);
                                reset.NewPassword = user.Password;
                                reset.IsDeleted = false;
                                reset.CreatedBy = user.Id;
                                reset.CreatedOn = DateTime.Now;
                                reset.IsDeleted = false;

                                db.Add(reset);
                                db.SaveChanges();

                                dbTran.Commit();
                                response.message = "success change password";
                                response.statusCode = System.Net.HttpStatusCode.OK;
                                response.data = data;
                            }
                            else
                            {
                                throw new Exception("You have already used the password, use another password!");
                            }
                        }
                        else
                        {
                            throw new Exception("password does not match");
                        }
                    }
                    else
                    {
                        throw new Exception("cannot change password because data was null");
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
