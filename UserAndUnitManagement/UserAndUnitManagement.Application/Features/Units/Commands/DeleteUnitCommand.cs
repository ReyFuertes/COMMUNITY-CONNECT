using MediatR;
using System;

namespace UserAndUnitManagement.Application.Features.Units.Commands
{
    public class DeleteUnitCommand : IRequest<MediatR.Unit>
    {
        public Guid Id { get; set; }
    }
}