using MediatR;
using UserAndUnitManagement.Domain.Entities;
using System;

namespace UserAndUnitManagement.Application.Features.Vehicles.Queries
{
    public class GetVehicleByIdQuery : IRequest<Vehicle?>
    {
        public Guid Id { get; set; }
    }
}