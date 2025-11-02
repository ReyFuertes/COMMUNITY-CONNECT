using MediatR;
using UserAndUnitManagement.Domain.Entities;

namespace UserAndUnitManagement.Application.Features.Vehicles.Commands
{
    public class CreateVehicleCommand : IRequest<Vehicle>
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public Guid UserId { get; set; }
    }
}