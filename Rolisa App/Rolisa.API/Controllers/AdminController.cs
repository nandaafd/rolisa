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
    public class AdminController : ControllerBase
    {
        private AdminService admin;
        public AdminController(AdminService _admin) { admin = _admin; }
        [HttpGet]
        public VMResponse GetAll() => admin.GetAll();
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id) => admin.GetById(id);
        [HttpPost]
        public VMResponse Add(VMAdmin data) => admin.Create(data);
        [HttpPut]
        public VMResponse Edit(VMAdmin data) => admin.Update(data);
    }
}
