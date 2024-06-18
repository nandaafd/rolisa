using Microsoft.EntityFrameworkCore.Storage;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Service
{
    public class ArticleService
    {
        private VMResponse response = new VMResponse();
        private RolisaContext db;
        public ArticleService(RolisaContext _db) { db = _db; }
        public VMResponse GetByFilter(string filter)
        {
            try
            {
                List<VMArticle> article = (
                    from a in db.Articles
                    where a.IsDeleted == false && a.Tittle.Contains(filter ?? "") || a.Writer.Contains(filter ?? "")
                    select new VMArticle
                    {
                        Id = a.Id,
                        Tittle = a.Tittle,
                        Writer = a.Writer,
                        PublishDate = a.PublishDate,
                        Text = a.Text,
                        ImagePath = a.ImagePath,
                        CreatedBy = a.CreatedBy,
                        CreatedOn = a.CreatedOn,
                        ModifiedBy = a.ModifiedBy,
                        ModifiedOn = a.ModifiedOn,
                        DeletedBy = a.DeletedBy,
                        DeletedOn = a.DeletedOn,
                        IsDeleted = a.IsDeleted
                    }
                ).ToList();
                if (filter != null)
                {
                    response.statusCode = (article.Count > 0) ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NoContent;
                    response.message = (article.Count > 0) ? $"success fetched {article.Count} data product category" : "product category has no data!";
                    response.data = article;
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
                VMArticle? article = (
                    from a in db.Articles
                    where a.IsDeleted == false && a.Id == id
                    select new VMArticle
                    {
                        Id = a.Id,
                        Tittle = a.Tittle,
                        Writer = a.Writer,
                        PublishDate = a.PublishDate,
                        Text = a.Text,
                        ImagePath = a.ImagePath,
                        CreatedBy = a.CreatedBy,
                        CreatedOn = a.CreatedOn,
                        ModifiedBy = a.ModifiedBy,
                        ModifiedOn = a.ModifiedOn,
                        DeletedBy = a.DeletedBy,
                        DeletedOn = a.DeletedOn,
                        IsDeleted = a.IsDeleted
                    }
                ).FirstOrDefault();
                if (article != null)
                {
                    response.statusCode = System.Net.HttpStatusCode.OK;
                    response.message = "success get data by id" + id;
                    response.data = article;
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
        public VMResponse Create(VMArticle data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    Article article = new Article()
                    {
                        Tittle = data.Tittle,
                        Writer = data.Writer,
                        PublishDate = data.PublishDate,
                        Text = data.Text,
                        ImagePath = data.ImagePath,
                        CreatedBy = data.CreatedBy,
                        CreatedOn = DateTime.Now,
                        IsDeleted = false,
                    };

                    db.Add(article);
                    db.SaveChanges();
                    dbTran.Commit();

                    response.statusCode = System.Net.HttpStatusCode.Created;
                    response.message = "success create data article";
                    response.data = article;
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
        public VMResponse Update(VMArticle data)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMResponse response = GetById(data.Id);
                    object? dataResponse = response.data;
                    VMArticle existingData = (VMArticle)dataResponse;
                    if (existingData != null)
                    {
                        Article article = new Article()
                        {
                            Id = existingData.Id,
                            Tittle = data.Tittle,
                            Writer = data.Writer,
                            PublishDate = data.PublishDate,
                            Text = data.Text,
                            ImagePath = data.ImagePath,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,
                            IsDeleted = false,
                        };
                        db.Update(article);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = "success update data article";
                        response.data = article;
                    }
                    else
                    {
                        dbTran.Rollback();
                        response.statusCode = System.Net.HttpStatusCode.NoContent;
                        response.message = "cannot found data with id " + data.Id;
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
                    VMArticle existingData = (VMArticle)dataResponse;
                    if (existingData != null)
                    {
                        Article article = new Article()
                        {
                            Id = existingData.Id,
                            Tittle = existingData.Tittle,
                            Writer = existingData.Writer,
                            PublishDate = existingData.PublishDate,
                            Text = existingData.Text,
                            ImagePath = existingData.ImagePath,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = existingData.ModifiedBy,
                            ModifiedOn = existingData.ModifiedOn,
                            DeletedBy = userId,
                            DeletedOn = DateTime.Now,
                            IsDeleted = true,
                        };
                        db.Update(article);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.statusCode = System.Net.HttpStatusCode.OK;
                        response.message = "success delete data article";
                        response.data = article;
                    }
                    else
                    {
                        dbTran.Rollback();
                        response.statusCode = System.Net.HttpStatusCode.NoContent;
                        response.message = "cannot found data with id " + id;
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
