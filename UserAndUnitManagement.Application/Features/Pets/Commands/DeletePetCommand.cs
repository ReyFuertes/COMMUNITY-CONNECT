using MediatR;
using System;

namespace UserAndUnitManagement.Application.Features.Pets.Commands
{
    public class DeletePetCommand : IRequest<MediatR.Unit>
    {
        public Guid Id { get; set; }
    }
}