using MediatR;
using UserAndUnitManagement.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Unit = UserAndUnitManagement.Domain.Entities.Unit;

namespace UserAndUnitManagement.Application.Features.Units.Queries
{
    public class GetUnitByIdQueryHandler : IRequestHandler<GetUnitByIdQuery, Unit>
    {
        private readonly IRepository<Unit> _unitRepository;

        public GetUnitByIdQueryHandler(IRepository<Unit> unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<Unit> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitRepository.GetByIdAsync(request.Id);
        }
    }
}
