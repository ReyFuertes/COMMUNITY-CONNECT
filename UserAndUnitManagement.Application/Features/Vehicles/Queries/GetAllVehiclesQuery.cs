using MediatR;
using UserAndUnitManagement.Domain.Entities;
using System.Collections.Generic;

namespace UserAndUnitManagement.Application.Features.Vehicles.Queries
{
    public class GetAllVehiclesQuery : IRequest<IEnumerable<Vehicle>>
    {
    }
}