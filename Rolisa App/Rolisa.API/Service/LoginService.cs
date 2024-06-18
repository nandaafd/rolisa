using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Rolisa.DataModel;
using Rolisa.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Rolisa.API.Service
{
    public class LoginService
    {
        private VMResponse response = new VMResponse();
        private readonly UserService userService;
        private readonly string keys;
        public LoginService(UserService _userService, IConfiguration _config)
        {
            userService = _userService;
            keys = _config["Key"];
        }
        public string Login(VMUser data)
        {
            string token = "";
            VMResponse response = userService.GetByEmail(data.Email);
            object dataResponse = response.data;
            User existingData = (User)dataResponse;

            string existingPassword = existingData.Password;
            if (existingData != null)
            {
                if (BCrypt.Net.BCrypt.Verify(data.Password, existingPassword))
                {
                    token = CreateToken(data);
                }
            }
            else
            {
                token = "";
            }
            return token;
        }
        private string CreateToken(VMUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,  user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keys));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
