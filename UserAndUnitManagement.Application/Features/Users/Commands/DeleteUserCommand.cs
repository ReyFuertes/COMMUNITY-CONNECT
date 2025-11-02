using MediatR;
using System;

namespace UserAndUnitManagement.Application.Features.Users.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
