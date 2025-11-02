using MediatR;
using System.Collections.Generic;
using Unit = UserAndUnitManagement.Domain.Entities.Unit;

namespace UserAndUnitManagement.Application.Features.Units.Queries
{
    public class GetAllUnitsQuery : IRequest<IEnumerable<Unit>>
    {
    }
}
