using AuthenticationService.Models;
using AuthenticationService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(User user)
        {
            User u = new UserRepository().GetUser(user.Username);
            if (u == null)
                return NotFound("The user was not found."); 
            bool credentials = u.Password.Equals(user.Password);
            if (!credentials) return Unauthorized("The username/password combination was wrong.");
            return Ok(TokenManager.GenerateToken(user.Username));
        }
    }
}