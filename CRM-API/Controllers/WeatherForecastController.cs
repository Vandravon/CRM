using Microsoft.AspNetCore.Mvc;
/*using Microsoft.AspNetCore.Authorization;
using CRM_API.Models;*/

namespace CRM_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public WeatherForecastController(JwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }




        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

/*        [Authorize]
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }*/

/*        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser([FromBody] AuthorizeUser user)
        {
            var token = jwtAuthenticationManager.Authenticate(user.email, user.password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }*/
    }
}