using MediatR;
using MaintenanceService.Application.Dtos;

namespace MaintenanceService.Application.Features.WorkOrders.Commands.AssignWorkOrder;

public record AssignWorkOrderCommand : IRequest<WorkOrderDto>
{
    public Guid WorkOrderId { get; init; }
    public Guid? AssignedToId { get; init; } // Nullable, as it can be unassigned
}
