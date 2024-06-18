using Microsoft.EntityFrameworkCore.Storage;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Service
{
    public class CustomerService
    {
        private readonly RolisaContext db;
        private VMResponse response = new VMResponse();
        public CustomerService(RolisaContext _db) { db = _db; }
        public VMResponse GetByFilter(string filter)
        {
            try
            {
                if (filter != null)
                {
                    List<VMCustomer> customers = (
                        from c in db.Customers
                        join u in db.Users on c.UserId equals u.Id
                        join b in db.Biodata on u.BiodataId equals b.Id
                        join ca in db.CustomerAddresses on c.Id equals ca.CustomerId
                        where c.IsDeleted == false && b.Nickname.Contains(filter ?? "") || b.Fullname.Contains(filter ?? "")
                        select new VMCustomer
                        {
                            Id = u.Id,
                            UserId = u.Id,
                            CustomerFullname = b.Fullname,
                            CustomerPhone = b.Phone,
                            AddressLabel = ca.Label,
                            FullAddress = ca.FullAddress,
                            CustomerPostalCode = ca.PostalCode,
                            AddressIsMain = ca.IsMain,
                            CreatedBy = c.CreatedBy,
                            CreatedOn = c.CreatedOn,
                            ModifiedBy = c.ModifiedBy,
                            ModifiedOn = c.ModifiedOn,
                            DeletedBy = c.DeletedBy,
                            DeletedOn = c.DeletedOn,
                            IsDeleted = c.IsDeleted
                        }
                    ).ToList();
                    if (customers != null)
                    {
                        response.message = "success get customers data";
                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.data = customers;
                    }
                    else
                    {
                        throw new ArgumentNullException("customers data cannot found");
                    }
                }
                else
                {
                    throw new Exception("please input filter first!");
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
                VMCustomer? data = (
                    from c in db.Customers
                    join u in db.Users on c.UserId equals u.Id
                    join b in db.Biodata on u.BiodataId equals b.Id
                    join ca in db.CustomerAddresses on c.Id equals ca.CustomerId
                    where c.IsDeleted == false && c.Id == id
                    select new VMCustomer
                    {
                        Id = u.Id,
                        UserId = u.Id,
                        CustomerFullname = b.Fullname,
                        CustomerPhone = b.Phone,
                        AddressLabel = ca.Label,
                        FullAddress = ca.FullAddress,
                        CustomerPostalCode = ca.PostalCode,
                        AddressIsMain = ca.IsMain,
                        CreatedBy = c.CreatedBy,
                        CreatedOn = c.CreatedOn,
                        ModifiedBy = c.ModifiedBy,
                        ModifiedOn = c.ModifiedOn,
                        DeletedBy = c.DeletedBy,
                        DeletedOn = c.DeletedOn,
                        IsDeleted = c.IsDeleted
                    }
                ).FirstOrDefault();
                if (data != null)
                {
                    response.message = "success get customers data by id";
                    response.statusCode = System.Net.HttpStatusCode.OK;
                    response.data = data;
                }
                else
                {
                    throw new ArgumentNullException("customers data cannot found");
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return response;
        }
        public VMResponse Update(VMCustomer data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMResponse response = GetById(data.Id);
                    object dataRes = response.data;
                    VMCustomer existingData = (VMCustomer)dataRes;
                    if (existingData != null)
                    {
                        Biodatum biodatum = new Biodatum();
                        biodatum.Id = existingData.Id;
                        biodatum.Fullname = data.CustomerFullname;
                        biodatum.Nickname = data.CustomerNickname;
                        biodatum.Phone = data.CustomerPhone;
                        biodatum.CreatedBy = existingData.CreatedBy ?? 0;
                        biodatum.CreatedOn = existingData.CreatedOn ?? DateTime.Now;
                        biodatum.ModifiedBy = data.ModifiedBy ?? 0;
                        biodatum.ModifiedOn = DateTime.Now;
                        biodatum.IsDeleted = false;
                        db.Update(biodatum);
                        db.SaveChanges();

                        CustomerAddress address = new CustomerAddress();
                        address.Label = data.AddressLabel;
                        address.FullAddress = data.FullAddress;
                        address.PostalCode = data.CustomerPostalCode;
                        address.IsMain = data.AddressIsMain;
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
