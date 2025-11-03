using MediatR;
using UserAndUnitManagement.Domain.Entities;
using System;

namespace UserAndUnitManagement.Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<User>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public bool OptInToDirectory { get; set; }
        public bool ShowEmailInDirectory { get; set; }
    }
}
