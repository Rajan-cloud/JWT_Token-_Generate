using JWT_Token_Generate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Token_Generate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("Getdata")]
        public string Getdata()
        {
            return "Authenticate with jwt";
        }

        [HttpGet]
        [Route("details")]
        public string details()
        {
            return "Authenticate with jwt";
        }


        [Authorize]
        [HttpPost]
        public string Adduser(Users users)
        {
            return "User added with username" + "  "+ users.username;
        }


    }
}
