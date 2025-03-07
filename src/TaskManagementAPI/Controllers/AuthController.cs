using Microsoft.AspNetCore.Mvc;
using Serilog;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService jwtService;

        public AuthController(JwtService jwtService)
        {
            this.jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            Log.Information("Login attempt for user: {Username}", request.Username);
            if (request.Username != "admin" || request.Password != "admin")
            {
                Log.Warning("Login failed for user: {Username}", request.Username);
                return Unauthorized();
            }
            
            var token = jwtService.GenerateSecurityToken(request.Username, "Admin");
            Log.Information("Login successful for user: {Username}", request.Username);
            return Ok(new { Token = token });
        }

        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}