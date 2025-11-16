using MediatR;
using UserAndUnitManagement.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using UserAndUnitManagement.Domain.Entities;

namespace UserAndUnitManagement.Application.Features.Pets.Commands
{
    public class DeletePetCommandHandler : IRequestHandler<DeletePetCommand, MediatR.Unit>
    {
        private readonly IRepository<Pet> _petRepository;

        public DeletePetCommandHandler(IRepository<Pet> petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<MediatR.Unit> Handle(DeletePetCommand request, CancellationToken cancellationToken)
        {
            var pet = await _petRepository.GetByIdAsync(request.Id);
            if (pet == null)
            {
                // Handle not found scenario
                throw new Exception($"Pet with ID {request.Id} not found.");
            }

            await _petRepository.DeleteAsync(pet);
            return MediatR.Unit.Value;
        }
    }
}