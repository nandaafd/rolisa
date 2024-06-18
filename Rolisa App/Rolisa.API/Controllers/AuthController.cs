using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rolisa.API.Service;
using Rolisa.ViewModel;

namespace Rolisa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LoginService loginService;
        public AuthController(LoginService _loginService)
        {
            loginService = _loginService;
        }
        [HttpPost]
        public string Login (VMUser user) => loginService.Login(user);
    }
}
