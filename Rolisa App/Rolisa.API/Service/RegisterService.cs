using Microsoft.EntityFrameworkCore.Storage;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Service
{
    public class RegisterService
    {
        private readonly RolisaContext db;
        private VMResponse response = new VMResponse();
        public RegisterService(RolisaContext _db) { db = _db; }
        public VMResponse Create(VMRegister data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    if (data != null)
                    {
                        User user = new User();
                        Biodatum biodata = new Biodatum();
                        Customer customer = new Customer();

                        biodata.Fullname = data.Biodatum.Fullname;
                        biodata.Nickname = data.Biodatum.Nickname;
                        biodata.Phone = data.Biodatum.Phone;
                        db.Add(biodata);
                        db.SaveChanges();

                        user.BiodataId = biodata.Id;
                        user.RoleId = 3; //customer role id
                        user.Email = data.User.Email;
                        user.Password = BCrypt.Net.BCrypt.HashPassword(data.User.Password);
                        user.IsBlocked = false;
                        db.Add(user);
                        db.SaveChanges();

                        customer.UserId = user.Id;
                        customer.CreatedBy = user.Id;
                        db.Add(customer);
                        db.SaveChanges();

                        biodata.CreatedBy = user.Id;
                        db.SaveChanges();
                        user.CreatedBy = user.Id;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("gagal mendaftarkan akun!");
                    }
                    dbTran.Commit();
                    response.statusCode = System.Net.HttpStatusCode.Created;
                    response.data = data;
                    response.message = "anda berhasil mendaftarkan, silahkan masuk!";
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
