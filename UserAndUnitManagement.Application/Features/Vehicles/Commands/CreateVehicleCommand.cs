using MediatR;
using UserAndUnitManagement.Domain.Entities;

namespace UserAndUnitManagement.Application.Features.Vehicles.Commands
{
    public class CreateVehicleCommand : IRequest<Vehicle>
    {
        public required string Make { get; set; }
        public required string Model { get; set; }
        public required string PlateNumber { get; set; }
        public required string Color { get; set; }
        public int Year { get; set; }
        public Guid UserId { get; set; }
    }
}