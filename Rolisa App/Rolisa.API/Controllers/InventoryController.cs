using Microsoft.AspNetCore.Mvc;
using Rolisa.DataAccess;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : Controller
    {
        private DAInventory? inventory;
        public InventoryController(RolisaContext _db) 
        {
            inventory = new DAInventory(_db);
        }
        [HttpGet("[action]/{filter?}")]
        public VMResponse? GetByFilter(string filter) => inventory.GetByFilter(filter);
        [HttpGet]
        public VMResponse? GetAll() => inventory.GetByFilter();
        [HttpGet("[action]/{id?}")]
        public VMResponse? GetById(int id) => inventory.GetById(id);
        [HttpPost]
        public VMResponse? Add(VMInventory data) => inventory.Create(data);
        [HttpPut]
        public VMResponse? Edit(VMInventory data) => inventory.Update(data);
        [HttpDelete("[action]/{id?}/{userId?}")]
        public VMResponse Delete(int id, int userId) => inventory.Delete(id, userId);
    }
}
