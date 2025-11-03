using MediatR;
using System;
using UserAndUnitManagement.Application.Features.Users.Dtos;

namespace UserAndUnitManagement.Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto?>
    {
        public Guid Id { get; set; }
    }
}
