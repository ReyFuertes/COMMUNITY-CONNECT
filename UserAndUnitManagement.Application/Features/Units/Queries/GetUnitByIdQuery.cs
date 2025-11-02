using MediatR;
using System;
using Unit = UserAndUnitManagement.Domain.Entities.Unit;

namespace UserAndUnitManagement.Application.Features.Units.Queries
{
    public class GetUnitByIdQuery : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
