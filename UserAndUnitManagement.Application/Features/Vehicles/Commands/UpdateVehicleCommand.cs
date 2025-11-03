using MediatR;
using System;

namespace UserAndUnitManagement.Application.Features.Vehicles.Commands
{
    public class UpdateVehicleCommand : IRequest<MediatR.Unit>
    {
        public Guid Id { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public int Year { get; set; }
        public Guid UserId { get; set; }
    }
}