using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using UserAndUnitManagement.Application.Features.Users.Dtos;
using UserAndUnitManagement.Domain.Interfaces;
using UserAndUnitManagement.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace UserAndUnitManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var user = await _userRepository.GetUserByEmail(login.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            using (var sha256 = SHA256.Create())
            {
                var passwordWithSalt = login.Password + user.Salt; // Use the stored salt
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt));
                var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                if (hashedPassword != user.PasswordHash)
                {
                    return Unauthorized();
                }
            }

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"]!,
                audience: jwtSettings["Audience"]!,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
