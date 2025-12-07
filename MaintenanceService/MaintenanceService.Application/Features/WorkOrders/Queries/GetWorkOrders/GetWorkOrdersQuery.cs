using MediatR;
using MaintenanceService.Application.Dtos;
using MaintenanceService.Domain.Enums;

namespace MaintenanceService.Application.Features.WorkOrders.Queries.GetWorkOrders;

public class GetWorkOrdersQuery : IRequest<IEnumerable<WorkOrderDto>>
{
    public WorkOrderStatus? Status { get; set; }
    public WorkOrderUrgency? UrgencyLevel { get; set; }
    public WorkOrderCategory? Category { get; set; }
    public Guid? UnitId { get; set; }
    public Guid? RequesterId { get; set; }
    public Guid? AssignedToId { get; set; }
    public bool IncludeAttachments { get; set; } = false;
    public bool IncludeNotes { get; set; } = false;
}
