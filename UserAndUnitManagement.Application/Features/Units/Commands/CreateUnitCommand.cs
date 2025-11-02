using MediatR;
using System;
using UserAndUnitManagement.Domain.Entities;

namespace UserAndUnitManagement.Application.Features.Units.Commands
{
    public class CreateUnitCommand : IRequest<UserAndUnitManagement.Domain.Entities.Unit>
    {
        public string Name { get; set; }
        public UnitStatus Status { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
