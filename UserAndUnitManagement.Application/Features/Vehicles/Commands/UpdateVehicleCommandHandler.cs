using MediatR;
using UserAndUnitManagement.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using UserAndUnitManagement.Domain.Entities;

namespace UserAndUnitManagement.Application.Features.Vehicles.Commands
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, MediatR.Unit>
    {
        private readonly IRepository<Vehicle> _vehicleRepository;

        public UpdateVehicleCommandHandler(IRepository<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<MediatR.Unit> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(request.Id);
            if (vehicle == null)
            {
                // Handle not found scenario
                throw new Exception($"Vehicle with ID {request.Id} not found.");
            }

            vehicle.Make = request.Make;
            vehicle.Model = request.Model;
            vehicle.Year = request.Year;
            vehicle.UserId = request.UserId;

            await _vehicleRepository.UpdateAsync(vehicle);
            return MediatR.Unit.Value;
        }
    }
}