using Microsoft.AspNetCore.Mvc;
using UserAndUnitManagement.Application.Features.Users.Commands;
using UserAndUnitManagement.Application.Features.Users.Dtos;
using MediatR;

namespace UserAndUnitManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _mediator.Send(new LoginCommand { Email = loginDto.Email, Password = loginDto.Password });
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
