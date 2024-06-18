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
    public class UserController : ControllerBase
    {
        private UserService user;
        public UserController(UserService _user) { user = _user; }
        [HttpGet]
        public VMResponse GetAll() => user.GetAll();
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(int id) => user.GetById(id);
        [HttpGet("[action]/{email?}")]
        public VMResponse GetByEmail(string email) => user.GetByEmail(email);
        [HttpPut]
        public VMResponse ChangePassword(VMUser data) => user.ChangePassword(data);
    }
}
