using JWT_Token_Generate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace JWT_Token_Generate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        private readonly IConfiguration _config;
        public LoggingController(IConfiguration configuration)
        {
                _config = configuration;
        }

        private Users AuthenticateUsers(Users user)
        {
            Users _user = null;
            if(user.username=="Admin"&& user.Password=="12345")
            {
                _user= new Users { username="Rajan",Password="12345"};
            }
            return _user;
        }

        private string GenerateToken(Users users)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            //var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var credential = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(_config["Jwt:Issuer"],_config["jwt:Audience"], null, expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credential
                );
            return  new JwtSecurityTokenHandler().WriteToken(Token);

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Users user)
        {
            IActionResult responce = Unauthorized();

            var user1 = AuthenticateUsers(user);
            if(user1!=null)
            {
                var token = GenerateToken(user1);
                responce=Ok(new { token = token });
            }
            return responce;
        }
    }
}
