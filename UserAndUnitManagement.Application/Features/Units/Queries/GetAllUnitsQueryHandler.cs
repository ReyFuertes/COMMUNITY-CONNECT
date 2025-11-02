using MediatR;
using UserAndUnitManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unit = UserAndUnitManagement.Domain.Entities.Unit;

namespace UserAndUnitManagement.Application.Features.Units.Queries
{
    public class GetAllUnitsQueryHandler : IRequestHandler<GetAllUnitsQuery, IEnumerable<Unit>>
    {
        private readonly IRepository<Unit> _unitRepository;

        public GetAllUnitsQueryHandler(IRepository<Unit> unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<IEnumerable<Unit>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
        {
            return await _unitRepository.GetAllAsync();
        }
    }
}
