using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rolisa.DataAccess;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private DAAdmin admin;
        public AdminController(RolisaContext _db) { admin = new DAAdmin(_db); }
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id) => admin.GetById(id);
        [HttpPost]
        public VMResponse Add(VMAdmin data) => admin.Create(data);
        [HttpPut]
        public VMResponse Edit(VMAdmin data) => admin.Update(data);
    }
}
