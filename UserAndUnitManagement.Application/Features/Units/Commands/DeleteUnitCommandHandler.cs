using MediatR;
using UserAndUnitManagement.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Unit = UserAndUnitManagement.Domain.Entities.Unit;

namespace UserAndUnitManagement.Application.Features.Units.Commands
{
    public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand, MediatR.Unit>
    {
        private readonly IRepository<Unit> _unitRepository;

        public DeleteUnitCommandHandler(IRepository<Unit> unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<MediatR.Unit> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.GetByIdAsync(request.Id);
            if (unit == null)
            {
                // Handle not found scenario
                throw new Exception($"Unit with ID {request.Id} not found.");
            }

            await _unitRepository.DeleteAsync(unit);
            return MediatR.Unit.Value;
        }
    }
}