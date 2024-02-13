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
    public class DAProduct
    {
        private VMResponse response = new VMResponse();
        private readonly RolisaContext db;
        public DAProduct(RolisaContext _db)
        {
            db = _db;
        }
        public VMResponse GetByFilter(string filter)
        {
            try
            {
                List<VMProduct> product = (
                    from p in db.Products
                    join pc in db.ProductCategories on p.ProductCategoryId equals pc.Id
                    where p.IsDeleted == false && p.Name.Contains(filter ?? "")
                    select new VMProduct
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        StockQty = p.StockQty,
                        Price = p.Price,
                        ProductCategoryId = p.ProductCategoryId,
                        ProductCategoryName = pc.Name,
                        Image1 = p.Image1,
                        Image2 = p.Image2,
                        Image3 = p.Image3,
                        CreatedBy = p.CreatedBy,
                        CreatedOn = p.CreatedOn,
                        ModifiedBy = p.ModifiedBy,
                        ModifiedOn = p.ModifiedOn,
                        DeletedBy = p.DeletedBy,
                        DeletedOn = p.DeletedOn,
                        IsDeleted = p.IsDeleted,
                    }
                ).ToList();
                if (filter != null)
                {
                    response.statusCode = (product.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
                    response.message = (product.Count > 0) ? $"success fetched {product.Count} data inventory category" : "inventory category has no data!";
                    response.data = product;
                }
                else
                {
                    response.statusCode = System.Net.HttpStatusCode.NoContent;
                    response.message = "please input filter first!";
                }
            }
            catch(Exception ex)
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
                VMProduct? product = (
                    from p in db.Products
                    join pc in db.ProductCategories on p.ProductCategoryId equals pc.Id
                    where p.IsDeleted == false && p.Id == id
                    select new VMProduct
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        StockQty = p.StockQty,
                        Price = p.Price,
                        ProductCategoryId = p.ProductCategoryId,
                        ProductCategoryName = pc.Name,
                        Image1 = p.Image1,
                        Image2 = p.Image2,
                        Image3 = p.Image3,
                        CreatedBy = p.CreatedBy,
                        CreatedOn = p.CreatedOn,
                        ModifiedBy = p.ModifiedBy,
                        ModifiedOn = p.ModifiedOn,
                        DeletedBy = p.DeletedBy,
                        DeletedOn = p.DeletedOn,
                        IsDeleted = p.IsDeleted,
                    }
                ).FirstOrDefault();
                if (product != null)
                {
                    response.statusCode = System.Net.HttpStatusCode.OK;
                    response.message = "success get data product with id " + id;
                    response.data = product;
                }
                else
                {
                    response.statusCode = System.Net.HttpStatusCode.NotFound;
                    response.message = "cannot found data product with id " + id;
                }
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
            }
            return response;
        }
        public VMResponse Create(VMProduct data)
        {
            using(IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    Product product = new Product()
                    {
                        Name = data.Name,
                        Description = data.Description,
                        StockQty = data.StockQty,
                        Price = data.Price,
                        ProductCategoryId = data.ProductCategoryId,
                        Image1 = data.Image1,
                        Image2 = data.Image2,
                        Image3 = data.Image3,
                        CreatedBy = data.CreatedBy,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false
                    };
                    db.Add(product);
                    db.SaveChanges();
                    dbTran.Commit();

                    response.statusCode = System.Net.HttpStatusCode.Created;
                    response.message = "success create data product";
                    response.data = product;
                }
                catch(Exception ex)
                {
                    dbTran.Rollback();
                    response.message = ex.Message;
                    response.statusCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            return response;
        }
        public VMResponse Update(VMProduct data)
        {
            using(IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMResponse response = GetById(data.Id);
                    object? dataResponse = response.data;
                    VMProduct existingData = (VMProduct)dataResponse;

                    if (existingData != null)
                    {
                        Product product = new Product()
                        {
                            Id = existingData.Id,
                            Name = data.Name,
                            Description = data.Description,
                            StockQty = data.StockQty,
                            Price = data.Price,
                            ProductCategoryId = data.ProductCategoryId,
                            Image1 = data.Image1,
                            Image2 = data.Image2,
                            Image3 = data.Image3,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };

                        db.Update(product);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = "success update data with id" + data.Id;
                        response.data = product;
                    }
                    else
                    {
                        response.statusCode = System.Net.HttpStatusCode.NoContent;
                        response.message = $"data with id {data.Id} cannot be found!";
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
                    VMProduct existingData = (VMProduct)dataResponse;

                    if (existingData != null)
                    {
                        Product product = new Product()
                        {
                            Id = existingData.Id,
                            Name = existingData.Name,
                            Description = existingData.Description,
                            StockQty = existingData.StockQty,
                            Price = existingData.Price,
                            ProductCategoryId = existingData.ProductCategoryId,
                            Image1 = existingData.Image1,
                            Image2 = existingData.Image2,
                            Image3 = existingData.Image3,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = existingData.ModifiedBy,
                            ModifiedOn = existingData.ModifiedOn,
                            DeletedBy = userId,
                            DeletedOn = DateTime.Now,
                            IsDeleted = true
                        };

                        db.Update(product);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = "success delete data with id" + id;
                        response.data = product;
                    }
                    else
                    {
                        response.statusCode = System.Net.HttpStatusCode.NoContent;
                        response.message = $"data with id {id} cannot be found!";
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
