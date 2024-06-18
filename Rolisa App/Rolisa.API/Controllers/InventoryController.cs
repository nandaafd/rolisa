using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rolisa.API.Service;
using Rolisa.DataAccess;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : Controller
    {
        private InventoryService? inventory;
        public InventoryController(InventoryService _inventory) 
        {
            inventory = _inventory;
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
