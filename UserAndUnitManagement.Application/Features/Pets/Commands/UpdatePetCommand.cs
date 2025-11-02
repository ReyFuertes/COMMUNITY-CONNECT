using MediatR;
using System;

namespace UserAndUnitManagement.Application.Features.Pets.Commands
{
    public class UpdatePetCommand : IRequest<MediatR.Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public Guid UserId { get; set; }
    }
}