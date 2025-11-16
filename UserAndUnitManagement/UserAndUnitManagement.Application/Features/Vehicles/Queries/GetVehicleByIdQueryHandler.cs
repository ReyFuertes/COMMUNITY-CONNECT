using MediatR;
using UserAndUnitManagement.Domain.Entities;
using UserAndUnitManagement.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UserAndUnitManagement.Application.Features.Vehicles.Queries
{
    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, Vehicle?>
    {
        private readonly IRepository<Vehicle> _vehicleRepository;

        public GetVehicleByIdQueryHandler(IRepository<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Vehicle?> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _vehicleRepository.GetByIdAsync(request.Id);
        }
    }
}