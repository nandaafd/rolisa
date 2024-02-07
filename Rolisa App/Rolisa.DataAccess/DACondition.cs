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
    public class DACondition
    {
        private readonly RolisaContext db;
        private VMResponse response = new VMResponse();
        public DACondition(RolisaContext _db)
        {
            db = _db;
        }
        public VMResponse GetByFilter(string filter)
        {
            try
            {
                List<VMCondition> data = (
                    from ic in db.Conditions
                    where ic.IsDeleted == false && ic.Name.Contains(filter ?? "")
                    select new VMCondition
                    {
                        Id = ic.Id,
                        Name = ic.Name,
                        CreatedBy = ic.CreatedBy,
                        CreatedOn = ic.CreatedOn,
                        ModifiedBy = ic.ModifiedBy,
                        ModifiedOn = ic.ModifiedOn,
                        DeletedBy = ic.DeletedBy,
                        DeletedOn = ic.DeletedOn,
                        IsDeleted = ic.IsDeleted
                    }
                ).ToList();
                if (filter != null)
                {
                    response.statusCode = (data.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
                    response.message = (data.Count > 0) ? $"success fetched {data.Count} data condition" : "inventory condition has no data!";
                    response.data = data;
                }
                else
                {
                    throw new ArgumentNullException();
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
                VMCondition? data = (
                    from ic in db.Conditions
                    where ic.Id == id && ic.IsDeleted == false
                    select new VMCondition
                    {
                        Id = ic.Id,
                        Name = ic.Name,
                        CreatedBy = ic.CreatedBy,
                        CreatedOn = ic.CreatedOn,
                        ModifiedBy = ic.ModifiedBy,
                        ModifiedOn = ic.ModifiedOn,
                        DeletedBy = ic.DeletedBy,
                        DeletedOn = ic.DeletedOn,
                        IsDeleted = ic.IsDeleted
                    }
                ).FirstOrDefault();
                if (data != null)
                {
                    response.statusCode = System.Net.HttpStatusCode.OK;
                    response.message = "success get data by id" + id;
                    response.data = data;
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

        public VMResponse Create(VMCondition data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    Condition condition = new Condition()
                    {
                        Name = data.Name,
                        CreatedBy = data.CreatedBy,
                        IsDeleted = false
                    };
                    db.Add(condition);
                    db.SaveChanges();
                    dbTran.Commit();

                    response.statusCode = System.Net.HttpStatusCode.Created;
                    response.message = "success create data inventory category";
                    response.data = condition;
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

        public VMResponse Update(VMCondition data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                VMResponse response = GetById(data.Id);
                object? dataResponse = response.data;
                VMCondition existingData = (VMCondition)dataResponse;
                try
                {
                    Condition condition = new Condition()
                    {
                        Id = existingData.Id,
                        Name = data.Name,
                        CreatedBy = existingData.CreatedBy,
                        CreatedOn = existingData.CreatedOn,
                        ModifiedBy = data.ModifiedBy,
                        ModifiedOn = DateTime.Now,
                        IsDeleted = false
                    };
                    db.Update(condition);
                    db.SaveChanges();
                    dbTran.Commit();

                    response.statusCode = System.Net.HttpStatusCode.OK;
                    response.message = "success update data inventory condition";
                    response.data = condition;
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
                VMCondition existingData = (VMCondition)dataResponse;
                try
                {
                    Condition condition = new Condition()
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
                    db.Update(condition);
                    db.SaveChanges();
                    dbTran.Commit();

                    response.statusCode = System.Net.HttpStatusCode.OK;
                    response.message = "success delete data inventory condition";
                    response.data = condition;
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
