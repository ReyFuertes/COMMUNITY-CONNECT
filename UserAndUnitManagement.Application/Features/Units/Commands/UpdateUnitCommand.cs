using MediatR;
using System;
using UserAndUnitManagement.Domain.Entities;

namespace UserAndUnitManagement.Application.Features.Units.Commands
{
    public class UpdateUnitCommand : IRequest<MediatR.Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public UnitStatus Status { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
