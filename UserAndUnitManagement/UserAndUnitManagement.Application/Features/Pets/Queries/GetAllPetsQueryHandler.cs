using MediatR;
using UserAndUnitManagement.Domain.Entities;
using UserAndUnitManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserAndUnitManagement.Application.Features.Pets.Queries
{
    public class GetAllPetsQueryHandler : IRequestHandler<GetAllPetsQuery, IEnumerable<Pet>>
    {
        private readonly IRepository<Pet> _petRepository;

        public GetAllPetsQueryHandler(IRepository<Pet> petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<IEnumerable<Pet>> Handle(GetAllPetsQuery request, CancellationToken cancellationToken)
        {
            return await _petRepository.GetAllAsync();
        }
    }
}