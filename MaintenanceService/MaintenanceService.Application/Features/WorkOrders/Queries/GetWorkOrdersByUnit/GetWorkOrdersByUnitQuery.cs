using MediatR;
using MaintenanceService.Application.Dtos;

namespace MaintenanceService.Application.Features.WorkOrders.Queries.GetWorkOrdersByUnit;

public record GetWorkOrdersByUnitQuery : IRequest<IEnumerable<WorkOrderDto>>
{
    public Guid UnitId { get; init; }
    public bool IncludeAttachments { get; set; } = false;
    public bool IncludeNotes { get; set; } = false;
}
