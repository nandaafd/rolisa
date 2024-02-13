using Microsoft.EntityFrameworkCore.Storage;
using Rolisa.DataModel;
using Rolisa.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rolisa.DataAccess
{
    public class DAVoucher
    {
        private VMResponse response = new VMResponse();
        private readonly RolisaContext db;
        public DAVoucher(RolisaContext _db) { db = _db; }
        public VMResponse GetByFilter(string filter)
        {
            try
            {
                List<VMVoucher>? voucher = (
                    from v in db.Vouchers
                    where v.IsDeleted == false && v.Name.Contains(filter ?? "")
                    select new VMVoucher
                    {
                        Id = v.Id,
                        Name = v.Name,
                        Description = v.Description,
                        StartDate = v.StartDate,
                        EndDate = v.EndDate,
                        Code = v.Code,
                        DiscountPresentage = v.DiscountPresentage,
                        CreatedBy = v.CreatedBy,
                        CreatedOn = v.CreatedOn,
                        ModifiedBy = v.ModifiedBy,
                        ModifiedOn = v.ModifiedOn,
                        DeletedBy = v.DeletedBy,
                        DeletedOn = v.DeletedOn,
                        IsDeleted = v.IsDeleted
                    }
                ).ToList();
                if (filter != null)
                {
                    response.statusCode = (voucher.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
                    response.message = (voucher.Count > 0) ? $"success fetched {voucher.Count} data voucher" : "voucher has no data!";
                    response.data = voucher;
                }
                else
                {
                    response.statusCode = System.Net.HttpStatusCode.NoContent;
                    response.message = "please input filter first!";
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public VMResponse GetAll() => GetByFilter("");
        public VMResponse GetById(int id)
        {
            try
            {
                VMVoucher? voucher = (
                    from v in db.Vouchers
                    where v.IsDeleted == false && v.Id == id
                    select new VMVoucher
                    {
                        Id = v.Id,
                        Name = v.Name,
                        Description = v.Description,
                        StartDate = v.StartDate,
                        EndDate = v.EndDate,
                        Code = v.Code,
                        DiscountPresentage = v.DiscountPresentage,
                        CreatedBy = v.CreatedBy,
                        CreatedOn = v.CreatedOn,
                        ModifiedBy = v.ModifiedBy,
                        ModifiedOn = v.ModifiedOn,
                        DeletedBy = v.DeletedBy,
                        DeletedOn = v.DeletedOn,
                        IsDeleted = v.IsDeleted
                    }
                ).FirstOrDefault();

                if (voucher != null)
                {
                    response.statusCode = System.Net.HttpStatusCode.OK;
                    response.message = "success get data by id" + id;
                    response.data = voucher;
                }
                else
                {
                    response.statusCode = System.Net.HttpStatusCode.NoContent;
                    response.message = $"data with id {id} not found";
                }
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return response;
        }

        public VMResponse Create(VMVoucher data)
        {
            using(IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    Voucher voucher = new Voucher()
                    {
                        Name = data.Name,
                        Code = data.Code,
                        Description = data.Description,
                        StartDate = data.StartDate,
                        EndDate = data.EndDate,
                        DiscountPresentage = data.DiscountPresentage,
                        CreatedBy = data.CreatedBy,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false
                    };
                    db.Add(voucher);
                    db.SaveChanges();
                    dbTran.Commit();

                    response.statusCode = System.Net.HttpStatusCode.Created;
                    response.message = "success create data inventory category";
                    response.data = voucher;
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
        public VMResponse Update(VMVoucher data)
        {
            using(IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMResponse response = GetById(data.Id);
                    object? dataResponse = response.data;
                    VMVoucher existingData = (VMVoucher)dataResponse;
                    if (existingData != null)
                    {
                        Voucher voucher = new Voucher()
                        {
                            Id = existingData.Id,
                            Name = data.Name,
                            Code = data.Code,
                            Description = data.Description,
                            StartDate = data.StartDate,
                            EndDate = data.EndDate,
                            DiscountPresentage = data.DiscountPresentage,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        db.Update(voucher);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.statusCode = System.Net.HttpStatusCode.Created;
                        response.message = "success update data voucher";
                        response.data = voucher;
                    }
                    else
                    {
                        response.statusCode = System.Net.HttpStatusCode.NoContent;
                        response.message = "cannot update data voucher, data not found";
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
                try
                {
                    VMResponse response = GetById(id);
                    object? dataResponse = response.data;
                    VMVoucher existingData = (VMVoucher)dataResponse;
                    if (existingData != null)
                    {
                        Voucher voucher = new Voucher()
                        {
                            Id = existingData.Id,
                            Name = existingData.Name,
                            Code = existingData.Code,
                            Description = existingData.Description,
                            StartDate = existingData.StartDate,
                            EndDate = existingData.EndDate,
                            DiscountPresentage = existingData.DiscountPresentage,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = existingData.ModifiedBy,
                            ModifiedOn = existingData.ModifiedOn,
                            DeletedBy = userId,
                            DeletedOn = DateTime.Now,
                            IsDeleted = true
                        };
                        db.Update(voucher);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.statusCode = System.Net.HttpStatusCode.Created;
                        response.message = "success delete data voucher";
                        response.data = voucher;
                    }
                    else
                    {
                        response.statusCode = System.Net.HttpStatusCode.NoContent;
                        response.message = "cannot delete data voucher, data not found";
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
