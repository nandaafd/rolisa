using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rolisa.DataAccess;
using Rolisa.DataModel;
using Rolisa.ViewModel;

namespace Rolisa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private DARegister register;
        public RegisterController(RolisaContext _db) { register = new DARegister(_db); }
        [HttpPost]
        public VMResponse Register(VMRegister data) => register.Create(data);
    }
}
