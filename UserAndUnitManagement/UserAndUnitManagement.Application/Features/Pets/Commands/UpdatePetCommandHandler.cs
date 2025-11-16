using MediatR;
using UserAndUnitManagement.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using UserAndUnitManagement.Domain.Entities;

namespace UserAndUnitManagement.Application.Features.Pets.Commands
{
    public class UpdatePetCommandHandler : IRequestHandler<UpdatePetCommand, MediatR.Unit>
    {
        private readonly IRepository<Pet> _petRepository;

        public UpdatePetCommandHandler(IRepository<Pet> petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<MediatR.Unit> Handle(UpdatePetCommand request, CancellationToken cancellationToken)
        {
            var pet = await _petRepository.GetByIdAsync(request.Id);
            if (pet == null)
            {
                // Handle not found scenario, e.g., throw an exception or return a specific result
                throw new Exception($"Pet with ID {request.Id} not found.");
            }

            pet.Name = request.Name;
            pet.Species = request.Species;
            pet.UserId = request.UserId;

            await _petRepository.UpdateAsync(pet);
            return MediatR.Unit.Value;
        }
    }
}