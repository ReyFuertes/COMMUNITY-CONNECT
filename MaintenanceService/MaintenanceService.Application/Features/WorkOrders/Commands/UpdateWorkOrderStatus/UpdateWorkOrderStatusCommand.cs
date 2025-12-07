using MediatR;
using MaintenanceService.Application.Dtos;
using MaintenanceService.Domain.Enums;

namespace MaintenanceService.Application.Features.WorkOrders.Commands.UpdateWorkOrderStatus;

public record UpdateWorkOrderStatusCommand : IRequest<WorkOrderDto>
{
    public Guid WorkOrderId { get; init; }
    public WorkOrderStatus NewStatus { get; init; }
    public Guid? UpdatedById { get; init; } // Optional: to track who updated the status
}
