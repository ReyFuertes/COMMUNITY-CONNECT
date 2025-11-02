using MediatR;
using UserAndUnitManagement.Domain.Entities;
using UserAndUnitManagement.Domain.Interfaces;

namespace UserAndUnitManagement.Application.Features.Vehicles.Commands
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Vehicle>
    {
        private readonly IRepository<Vehicle> _vehicleRepository;

        public CreateVehicleCommandHandler(IRepository<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Vehicle> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = new Vehicle
            {
                Make = request.Make,
                Model = request.Model,
                Year = request.Year,
                UserId = request.UserId
            };

            await _vehicleRepository.AddAsync(vehicle);
            return vehicle;
        }
    }
}