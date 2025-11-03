using Microsoft.AspNetCore.Mvc;
using UserAndUnitManagement.Domain.Entities;
using System.Threading.Tasks;
using MediatR;
using UserAndUnitManagement.Application.Features.Users.Commands;
using UserAndUnitManagement.Application.Features.Users.Queries;

using Microsoft.AspNetCore.Authorization;
using UserAndUnitManagement.Application.Features.Users.Dtos;

namespace UserAndUnitManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var userDtos = await _mediator.Send(new GetAllUsersQuery());
            return Ok(userDtos);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            var userDto = await _mediator.Send(new GetUserByIdQuery { Id = id });
            if (userDto == null)
            {
                return NotFound();
            }
            return Ok(userDto);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(CreateUserDto createUserDto)
        {
            var command = new CreateUserCommand
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                Password = createUserDto.Password,
                Role = (Domain.Entities.UserRole)createUserDto.Role,
                IsActive = createUserDto.IsActive,
                OptInToDirectory = createUserDto.OptInToDirectory,
                ShowEmailInDirectory = createUserDto.ShowEmailInDirectory
            };
            var user = await _mediator.Send(command);
            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = (int)user.Role,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate,
                LastModifiedDate = user.LastModifiedDate,
                OptInToDirectory = user.OptInToDirectory,
                ShowEmailInDirectory = user.ShowEmailInDirectory
            };
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UpdateUserDto updateUserDto)
        {
            if (id != updateUserDto.Id)
            {
                return BadRequest();
            }
            var command = new UpdateUserCommand
            {
                Id = updateUserDto.Id,
                FirstName = updateUserDto.FirstName,
                LastName = updateUserDto.LastName,
                Role = (Domain.Entities.UserRole)updateUserDto.Role,
                IsActive = updateUserDto.IsActive,
                OptInToDirectory = updateUserDto.OptInToDirectory,
                ShowEmailInDirectory = updateUserDto.ShowEmailInDirectory
            };
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _mediator.Send(new DeleteUserCommand { Id = id });
            return NoContent();
        }
    }
}
