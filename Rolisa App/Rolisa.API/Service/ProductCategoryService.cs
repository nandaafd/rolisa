using Microsoft.EntityFrameworkCore.Storage;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Service
{
    public class ProductCategoryService
    {
        private VMResponse response = new VMResponse();
        private readonly RolisaContext db;
        public ProductCategoryService(RolisaContext _db)
        {
            db = _db;
        }
        public VMResponse GetByFilter(string filter)
        {
            try
            {
                List<VMProductCategory> pcategory = (
                    from pc in db.ProductCategories
                    where pc.IsDeleted == false && pc.Name.Contains(filter ?? "")
                    select new VMProductCategory
                    {
                        Id = pc.Id,
                        Name = pc.Name,
                        CreatedBy = pc.CreatedBy,
                        CreatedOn = pc.CreatedOn,
                        ModifiedBy = pc.ModifiedBy,
                        ModifiedOn = pc.ModifiedOn,
                        DeletedBy = pc.DeletedBy,
                        DeletedOn = pc.DeletedOn,
                        IsDeleted = pc.IsDeleted
                    }
                ).ToList();
                if (filter != null)
                {
                    response.statusCode = (pcategory.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
                    response.message = (pcategory.Count > 0) ? $"success fetched {pcategory.Count} data product category" : "product category has no data!";
                    response.data = pcategory;
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
                VMProductCategory? pcategory = (
                    from pc in db.ProductCategories
                    where pc.IsDeleted == false && pc.Id == id
                    select new VMProductCategory
                    {
                        Id = pc.Id,
                        Name = pc.Name,
                        CreatedBy = pc.CreatedBy,
                        CreatedOn = pc.CreatedOn,
                        ModifiedBy = pc.ModifiedBy,
                        ModifiedOn = pc.ModifiedOn,
                        DeletedBy = pc.DeletedBy,
                        DeletedOn = pc.DeletedOn,
                        IsDeleted = pc.IsDeleted
                    }
                ).FirstOrDefault();

                if (pcategory != null)
                {
                    response.statusCode = System.Net.HttpStatusCode.OK;
                    response.message = "success get data by id" + id;
                    response.data = pcategory;
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
        public VMResponse Create(VMProductCategory data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    ProductCategory pcategory = new ProductCategory()
                    {
                        Name = data.Name,
                        CreatedBy = data.CreatedBy,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false
                    };
                    db.Add(pcategory);
                    db.SaveChanges();
                    dbTran.Commit();

                    response.statusCode = System.Net.HttpStatusCode.Created;
                    response.message = "success create data product category";
                    response.data = pcategory;
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
        public VMResponse Update(VMProductCategory data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                VMResponse response = GetById(data.Id);
                object? dataResponse = response.data;
                VMProductCategory existingData = (VMProductCategory)dataResponse;
                try
                {
                    if (existingData != null)
                    {
                        ProductCategory pcategory = new ProductCategory()
                        {
                            Id = existingData.Id,
                            Name = data.Name,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        db.Update(pcategory);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = "success update data inventory category";
                        response.data = pcategory;
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
                VMProductCategory existingData = (VMProductCategory)dataResponse;
                try
                {
                    if (existingData != null)
                    {
                        ProductCategory pcategory = new ProductCategory()
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
                        db.Update(pcategory);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = "success delete data product category";
                        response.data = pcategory;
                    }
                    else
                    {
                        response.statusCode = System.Net.HttpStatusCode.NoContent;
                        response.message = "cannot delete data product category, data not found";
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
