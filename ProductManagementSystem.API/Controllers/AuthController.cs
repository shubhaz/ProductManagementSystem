using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProductManagementSystem.Application.DTOs.Auth;
using ProductManagementSystem.Infrastructure.Identity;

namespace ProductManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly JwtSettings _jwtSettings;

        public AuthController(
            JwtTokenGenerator jwtTokenGenerator,
            IOptions<JwtSettings> jwtSettings)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _jwtSettings = jwtSettings.Value;
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequestDto request)
        {
            string role;

            if (request.Username == "admin"
                && request.Password == "admin123")
            {
                role = "Admin";
            }
            else if (request.Username == "user"
                     && request.Password == "user123")
            {
                role = "User";
            }
            else
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _jwtTokenGenerator.GenerateToken(
                request.Username,
                role);

            return Ok(new LoginResponseDto
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(
                    _jwtSettings.ExpiryMinutes)
            });
        }
    }
}
