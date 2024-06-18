using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rolisa.API.Service;
using Rolisa.DataAccess;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private RoleService role;
        public RoleController(RoleService _role) { role = _role; }
        [HttpGet("[action]/{filter?}")]
        public VMResponse? GetByFilter(string filter) => role.GetByFilter(filter);
        [HttpGet]
        public VMResponse? GetAll() => role.GetAll();
        [HttpGet("[action]/{id?}")]
        public VMResponse? GetById(int id) => role.GetById(id);
        [HttpPost]
        public VMResponse? Add(VMRole data) => role.Create(data);
        [HttpPut]
        public VMResponse? Edit(VMRole data) => role.Update(data);
        [HttpDelete("[action]/{id?}/{userId?}")]
        public VMResponse Delete(int id, int userId) => role.Delete(id, userId);
    }
}
