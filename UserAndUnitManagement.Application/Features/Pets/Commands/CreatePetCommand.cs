using MediatR;
using UserAndUnitManagement.Domain.Entities;

namespace UserAndUnitManagement.Application.Features.Pets.Commands
{
    public class CreatePetCommand : IRequest<Pet>
    {
        public required string Name { get; set; }
        public required string Species { get; set; }
        public required string Breed { get; set; }
        public required string PhotoUrl { get; set; }
        public Guid UserId { get; set; }
    }
}