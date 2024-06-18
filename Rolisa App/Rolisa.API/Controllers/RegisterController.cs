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
    public class RegisterController : ControllerBase
    {
        private RegisterService register;
        public RegisterController(RegisterService _register) { register = _register; }
        [HttpPost]
        public VMResponse Register(VMRegister data) => register.Create(data);
    }
}
