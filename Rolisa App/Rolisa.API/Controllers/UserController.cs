using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rolisa.DataAccess;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DAUser user;
        public UserController(RolisaContext _db) { user = new DAUser(_db); }
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id) => user.GetById(id);

        [HttpPut]
        public VMResponse ChangePassword(VMUser data) => user.ChangePassword(data);
    }
}
