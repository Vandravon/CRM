using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CRM_API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CRM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        public CrmContext context = new();

        public LoginController(JwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult AuthUser([FromBody] AuthorizeUser user)
        {
            var token = jwtAuthenticationManager.Authenticate(user.email, user.password);
            if (token == null)
            {
                return Unauthorized();
            }
            User userCheck = context.Users.Single(u => u.email == user.email);
            userCheck.password = token.ToString(); // Modifie le userCheck de l'User dans la table Users pour lui mettre la valeur du token
            context.SaveChanges();
            return Ok(token);
        }

    }
}
