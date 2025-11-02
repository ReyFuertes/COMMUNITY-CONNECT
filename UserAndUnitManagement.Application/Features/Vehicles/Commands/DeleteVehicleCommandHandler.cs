using MediatR;
using UserAndUnitManagement.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using UserAndUnitManagement.Domain.Entities;

namespace UserAndUnitManagement.Application.Features.Vehicles.Commands
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, MediatR.Unit>
    {
        private readonly IRepository<Vehicle> _vehicleRepository;

        public DeleteVehicleCommandHandler(IRepository<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<MediatR.Unit> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(request.Id);
            if (vehicle == null)
            {
                // Handle not found scenario
                throw new Exception($"Vehicle with ID {request.Id} not found.");
            }

            await _vehicleRepository.DeleteAsync(vehicle);
            return MediatR.Unit.Value;
        }
    }
}