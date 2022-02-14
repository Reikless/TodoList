using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoList.Models;

namespace TodoList
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {

        private readonly IJwtAuthentificationService _jwtAuthentificationService;
        private readonly IConfiguration _config;

        public AuthentificationController(IJwtAuthentificationService JwtAuthentificationService, IConfiguration config)
        {
            _jwtAuthentificationService = JwtAuthentificationService;
            _config = config;
        }

        [HttpPost]
        [Route ("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _jwtAuthentificationService.Authenticate(model.Email, model.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                };
                var token = _jwtAuthentificationService.GenerateToken(_config["Jwt:Key"], claims);
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
