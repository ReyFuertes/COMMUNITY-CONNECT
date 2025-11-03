using MediatR;
using System;
using UserAndUnitManagement.Domain.Entities;

namespace UserAndUnitManagement.Application.Features.Units.Commands
{
    public class UpdateUnitCommand : IRequest<MediatR.Unit>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public UnitStatus Status { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string ZipCode { get; set; }
    }
}
