using MediatR;
using UserAndUnitManagement.Domain.Entities;
using System;

namespace UserAndUnitManagement.Application.Features.Users.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public bool OptInToDirectory { get; set; }
        public bool ShowEmailInDirectory { get; set; }
    }
}
