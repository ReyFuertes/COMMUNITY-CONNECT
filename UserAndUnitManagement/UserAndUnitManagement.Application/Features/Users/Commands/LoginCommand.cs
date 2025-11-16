using MediatR;

namespace UserAndUnitManagement.Application.Features.Users.Commands
{
    public class LoginCommand : IRequest<string>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}