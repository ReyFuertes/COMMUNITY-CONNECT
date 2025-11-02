using MediatR;
using UserAndUnitManagement.Domain.Entities;
using System.Collections.Generic;

namespace UserAndUnitManagement.Application.Features.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<User>>
    {
    }
}
