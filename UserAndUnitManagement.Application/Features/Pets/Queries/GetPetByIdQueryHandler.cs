using MediatR;
using UserAndUnitManagement.Domain.Entities;
using UserAndUnitManagement.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace UserAndUnitManagement.Application.Features.Pets.Queries
{
    public class GetPetByIdQueryHandler : IRequestHandler<GetPetByIdQuery, Pet?>
    {
        private readonly IRepository<Pet> _petRepository;

        public GetPetByIdQueryHandler(IRepository<Pet> petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<Pet?> Handle(GetPetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _petRepository.GetByIdAsync(request.Id);
        }
    }
}