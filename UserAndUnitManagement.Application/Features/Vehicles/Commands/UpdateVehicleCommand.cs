using MediatR;
using System;

namespace UserAndUnitManagement.Application.Features.Vehicles.Commands
{
    public class UpdateVehicleCommand : IRequest<MediatR.Unit>
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public Guid UserId { get; set; }
    }
}