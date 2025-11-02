using MediatR;
using UserAndUnitManagement.Domain.Entities;
using System;

namespace UserAndUnitManagement.Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public Guid Id { get; set; }
    }
}
