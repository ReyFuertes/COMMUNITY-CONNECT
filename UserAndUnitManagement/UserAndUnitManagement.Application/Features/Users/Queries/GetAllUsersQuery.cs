using MediatR;
using System.Collections.Generic;
using UserAndUnitManagement.Application.Features.Users.Dtos;

namespace UserAndUnitManagement.Application.Features.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {
    }
}
