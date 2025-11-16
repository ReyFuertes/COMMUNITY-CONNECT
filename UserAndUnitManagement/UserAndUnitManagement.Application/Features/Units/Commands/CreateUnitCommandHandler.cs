using MediatR;
using UserAndUnitManagement.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Unit = UserAndUnitManagement.Domain.Entities.Unit;

namespace UserAndUnitManagement.Application.Features.Units.Commands
{
    public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, Unit>
    {
        private readonly IRepository<Unit> _unitRepository;

        public CreateUnitCommandHandler(IRepository<Unit> unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<Unit> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = new Unit
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Status = request.Status,
                Address = request.Address,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode,
                CreatedDate = DateTime.UtcNow
            };

            await _unitRepository.AddAsync(unit);
            return unit;
        }
    }
}
