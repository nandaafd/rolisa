using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rolisa.DataAccess;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private DAProductCategory? productCategory;

        public ProductCategoryController(RolisaContext _db)
        {
            productCategory = new DAProductCategory(_db);
        }
        [HttpGet]
        public VMResponse GetAll() => productCategory.GetAll();
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id) => productCategory.GetById(id);
        [HttpGet("[action]/{filter?}")]
        public VMResponse GetByFilter(string filter) => productCategory.GetByFilter(filter);
        [HttpPost]
        public VMResponse Add(VMProductCategory data) => productCategory.Create(data);
        [HttpPut]
        public VMResponse Edit(VMProductCategory data) => productCategory.Update(data);
        [HttpDelete("[action]/{id?}/{userId?}")]
        public VMResponse Delete(int id, int userId) => productCategory.Delete(id, userId);
    }
}
