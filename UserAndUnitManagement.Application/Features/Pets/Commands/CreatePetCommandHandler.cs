using MediatR;
using UserAndUnitManagement.Domain.Entities;
using UserAndUnitManagement.Domain.Interfaces;

namespace UserAndUnitManagement.Application.Features.Pets.Commands
{
    public class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, Pet>
    {
        private readonly IRepository<Pet> _petRepository;

        public CreatePetCommandHandler(IRepository<Pet> petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<Pet> Handle(CreatePetCommand request, CancellationToken cancellationToken)
        {
            var pet = new Pet
            {
                Name = request.Name,
                Species = request.Species,
                Breed = request.Breed,
                PhotoUrl = request.PhotoUrl,
                UserId = request.UserId
            };

            await _petRepository.AddAsync(pet);
            return pet;
        }
    }
}