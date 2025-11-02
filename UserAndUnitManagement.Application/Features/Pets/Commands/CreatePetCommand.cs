using MediatR;
using UserAndUnitManagement.Domain.Entities;

namespace UserAndUnitManagement.Application.Features.Pets.Commands
{
    public class CreatePetCommand : IRequest<Pet>
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public Guid UserId { get; set; }
    }
}