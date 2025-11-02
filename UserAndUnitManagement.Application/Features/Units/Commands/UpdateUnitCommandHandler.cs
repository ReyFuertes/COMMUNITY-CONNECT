using MediatR;
using UserAndUnitManagement.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Unit = UserAndUnitManagement.Domain.Entities.Unit;

namespace UserAndUnitManagement.Application.Features.Units.Commands
{
    public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, MediatR.Unit>
    {
        private readonly IRepository<Unit> _unitRepository;

        public UpdateUnitCommandHandler(IRepository<Unit> unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<MediatR.Unit> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.GetByIdAsync(request.Id);

            if (unit == null)
            {
                throw new ApplicationException($"Unit with id {request.Id} not found.");
            }

            unit.Name = request.Name;
            unit.Status = request.Status;
            unit.Address = request.Address;
            unit.City = request.City;
            unit.State = request.State;
            unit.ZipCode = request.ZipCode;
            unit.LastModifiedDate = DateTime.UtcNow;

            await _unitRepository.UpdateAsync(unit);
            return MediatR.Unit.Value;
        }
    }
}
