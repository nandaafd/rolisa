using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rolisa.DataAccess;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryCategoryController : ControllerBase
    {
        private DAInventoryCategory? inventoryCategory;

        public InventoryCategoryController(RolisaContext _db) 
        {
            inventoryCategory = new DAInventoryCategory(_db);
        }
        [HttpGet]
        public VMResponse GetAll() => inventoryCategory.GetAll();
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id) => inventoryCategory.GetById(id);
        [HttpGet("[action]/{filter?}")]
        public VMResponse GetByFilter(string filter) => inventoryCategory.GetByFilter(filter);
        [HttpPost]
        public VMResponse Add(VMInventoryCategory data) => inventoryCategory.Create(data);
        [HttpPut]
        public VMResponse Edit(VMInventoryCategory data) => inventoryCategory.Update(data);
        [HttpDelete("[action]/{id?}/{userId?}")]
        public VMResponse Delete(int id, int userId) => inventoryCategory.Delete(id, userId);
    }
}
