using Microsoft.EntityFrameworkCore.Storage;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Service
{
    public class FavoriteService
    {
        private VMResponse response = new VMResponse();
        private readonly RolisaContext db;
        public FavoriteService(RolisaContext _db)
        {
            db = _db;
        }
        public VMResponse GetByFilter(string filter)
        {
            try
            {
                List<VMFavorite> data = (
                    from f in db.Favorites
                    join p in db.Products on f.ProductId equals p.Id
                    where f.IsDeleted == false && p.Name.Contains(filter ?? "")
                    select new VMFavorite
                    {
                        Id = f.Id,
                        ProductId = f.ProductId,
                        ProductName = p.Name,
                        CustomerId = f.CustomerId,
                        CreatedBy = f.CreatedBy,
                        CreatedOn = f.CreatedOn,
                        ModifiedBy = f.ModifiedBy,
                        ModifiedOn = f.ModifiedOn,
                        DeletedBy = f.DeletedBy,
                        DeletedOn = f.DeletedOn,
                        IsDeleted = f.IsDeleted,
                    }
                ).ToList();
                if (filter != null)
                {
                    response.statusCode = (data.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
                    response.message = (data.Count > 0) ? $"success fetched {data.Count} data product favorites" : "product favorites has no data!";
                    response.data = data;
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
                VMFavorite? data = (
                    from f in db.Favorites
                    join p in db.Products on f.ProductId equals p.Id
                    where f.IsDeleted == false && f.Id == id
                    select new VMFavorite
                    {
                        Id = f.Id,
                        ProductId = f.ProductId,
                        ProductName = p.Name,
                        CustomerId = f.CustomerId,
                        CreatedBy = f.CreatedBy,
                        CreatedOn = f.CreatedOn,
                        ModifiedBy = f.ModifiedBy,
                        ModifiedOn = f.ModifiedOn,
                        DeletedBy = f.DeletedBy,
                        DeletedOn = f.DeletedOn,
                        IsDeleted = f.IsDeleted,
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
        public VMResponse Create(VMFavorite data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    Favorite favorite = new Favorite()
                    {
                        ProductId = data.ProductId,
                        CustomerId = data.CustomerId,
                        CreatedBy = data.CreatedBy,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false
                    };
                    db.Add(favorite);
                    db.SaveChanges();
                    dbTran.Commit();

                    response.statusCode = System.Net.HttpStatusCode.Created;
                    response.message = "success create data product category";
                    response.data = favorite;
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
                    VMFavorite existingData = (VMFavorite)dataResponse;
                    if (existingData != null)
                    {
                        Favorite favorite = new Favorite()
                        {
                            Id = existingData.Id,
                            ProductId = existingData.ProductId,
                            CustomerId = existingData.CustomerId,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            DeletedBy = userId,
                            DeletedOn = DateTime.Now,
                            IsDeleted = true
                        };
                        db.Update(favorite);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = "success delete data product favorites";
                        response.data = favorite;
                    }
                    else
                    {
                        response.statusCode = System.Net.HttpStatusCode.NoContent;
                        response.message = "cannot find data product favorites with id " + id;
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
