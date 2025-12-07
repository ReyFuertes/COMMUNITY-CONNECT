using MediatR;
using MaintenanceService.Application.Dtos;
using MaintenanceService.Domain.Enums;

namespace MaintenanceService.Application.Features.WorkOrders.Commands.SubmitWorkOrder;

public record SubmitWorkOrderCommand : IRequest<WorkOrderDto>
{
    public string Description { get; init; }
    public WorkOrderCategory Category { get; init; }
    public WorkOrderUrgency UrgencyLevel { get; init; }
    public Guid UnitId { get; init; }
    public Guid RequesterId { get; init; }
    // Optional: public List<string> Attachments { get; init; } // for file paths or names
}
