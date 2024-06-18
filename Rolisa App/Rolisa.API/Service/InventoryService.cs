using Microsoft.EntityFrameworkCore.Storage;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Service
{
    public class InventoryService
    {
        private VMResponse response = new VMResponse();
        private readonly RolisaContext db;

        public InventoryService(RolisaContext _db)
        {
            db = _db;
        }
        public VMResponse GetByFilter(string? filter)
        {
            try
            {
                List<VMInventory>? data = (
                    from i in db.Inventories
                    join ic in db.InventoryCategories on i.InventoryCategoryId equals ic.Id
                    join c in db.Conditions on i.ConditionId equals c.Id
                    where i.IsDeleted == false && i.Name.Contains(filter ?? "")
                    select new VMInventory
                    {
                        Id = i.Id,
                        Name = i.Name,
                        InventoryCategoryId = i.InventoryCategoryId,
                        InventoryCategoryName = ic.Name,
                        ConditionId = i.ConditionId,
                        ConditionName = c.Name,
                        Qty = i.Qty,
                        ImagePath = i.ImagePath,
                        CreatedBy = i.CreatedBy,
                        CreatedOn = i.CreatedOn,
                        ModifiedBy = i.ModifiedBy,
                        ModifiedOn = i.ModifiedOn,
                        DeletedBy = i.DeletedBy,
                        DeletedOn = i.DeletedOn,
                        IsDeleted = i.IsDeleted
                    }
                ).ToList();
                if (filter == null)
                {
                    response.message = "please input your filter";
                    response.statusCode = System.Net.HttpStatusCode.NoContent;
                }
                else
                {
                    response.data = data;
                    response.message = (data.Count > 0) ? $"{data.Count} inventory success fetched!" : "Inventory Has no data!";
                    response.statusCode = (data.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
                }

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }
        public VMResponse GetByFilter() => GetByFilter("");
        public VMResponse GetById(int id)
        {
            try
            {
                VMInventory? data = (
                    from i in db.Inventories
                    join ic in db.InventoryCategories on i.InventoryCategoryId equals ic.Id
                    join c in db.Conditions on i.ConditionId equals c.Id
                    where i.IsDeleted == false && i.Id == id
                    select new VMInventory
                    {
                        Id = i.Id,
                        Name = i.Name,
                        InventoryCategoryId = i.InventoryCategoryId,
                        InventoryCategoryName = ic.Name,
                        ConditionId = i.ConditionId,
                        ConditionName = c.Name,
                        Qty = i.Qty,
                        ImagePath = i.ImagePath,
                        CreatedBy = i.CreatedBy,
                        CreatedOn = i.CreatedOn,
                        ModifiedBy = i.ModifiedBy,
                        ModifiedOn = i.ModifiedOn,
                        DeletedBy = i.DeletedBy,
                        DeletedOn = i.DeletedOn,
                        IsDeleted = i.IsDeleted
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
                response.statusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }
        public VMResponse Create(VMInventory data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    Inventory inventory = new Inventory()
                    {
                        Id = data.Id,
                        Name = data.Name,
                        InventoryCategoryId = data.InventoryCategoryId,
                        ConditionId = data.ConditionId,
                        Qty = data.Qty,
                        ImagePath = data.ImagePath,
                        CreatedBy = data.CreatedBy,
                        IsDeleted = false
                    };
                    db.Add(inventory);
                    db.SaveChanges();
                    dbTran.Commit();

                    response.statusCode = System.Net.HttpStatusCode.Created;
                    response.message = "success create data inventory";
                    response.data = inventory;
                }
                catch (Exception ex)
                {
                    response.message = ex.Message;
                    response.statusCode = System.Net.HttpStatusCode.InternalServerError;
                    dbTran.Rollback();
                }
            }

            return response;
        }
        public VMResponse Update(VMInventory data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMResponse? response = GetById(data.Id);
                    object? dataResponse = response.data;
                    VMInventory? existingData = (VMInventory?)dataResponse;
                    if (existingData != null)
                    {
                        Inventory inventory = new Inventory()
                        {
                            Id = existingData.Id,
                            Name = data.Name,
                            InventoryCategoryId = data.InventoryCategoryId,
                            ConditionId = data.ConditionId,
                            Qty = data.Qty,
                            ImagePath = data.ImagePath,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,
                            IsDeleted = false,
                        };
                        db.Update(inventory);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = $"success edit data with id {data.Id}";
                        response.data = data;
                    }
                    else
                    {
                        response.statusCode = System.Net.HttpStatusCode.NoContent;
                        response.message = $"data with id {data.Id} cannot be found";
                    }
                }
                catch (Exception ex)
                {
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
                    VMResponse? response = GetById(id);
                    object? dataResponse = response.data;
                    VMInventory? existingData = (VMInventory?)dataResponse;
                    if (existingData != null)
                    {
                        Inventory inventory = new Inventory()
                        {
                            Id = existingData.Id,
                            Name = existingData.Name,
                            InventoryCategoryId = existingData.InventoryCategoryId,
                            ConditionId = existingData.ConditionId,
                            Qty = existingData.Qty,
                            ImagePath = existingData.ImagePath,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = existingData.ModifiedBy,
                            ModifiedOn = existingData.ModifiedOn,
                            DeletedBy = userId,
                            DeletedOn = DateTime.Now,
                            IsDeleted = true,
                        };
                        db.Update(inventory);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = $"success delete data with id {id}";
                        response.data = dataResponse;
                    }
                }
                catch (Exception ex)
                {
                    response.message = ex.Message;
                    response.statusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            return response;
        }
    }
}
