using MediatR;
using System;

namespace UserAndUnitManagement.Application.Features.Vehicles.Commands
{
    public class DeleteVehicleCommand : IRequest<MediatR.Unit>
    {
        public Guid Id { get; set; }
    }
}