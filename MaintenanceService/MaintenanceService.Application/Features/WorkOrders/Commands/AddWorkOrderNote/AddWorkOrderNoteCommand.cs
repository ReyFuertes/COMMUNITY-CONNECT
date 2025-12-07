using MediatR;
using MaintenanceService.Application.Dtos;

namespace MaintenanceService.Application.Features.WorkOrders.Commands.AddWorkOrderNote;

public record AddWorkOrderNoteCommand : IRequest<WorkOrderDto>
{
    public Guid WorkOrderId { get; init; }
    public string Content { get; init; }
    public Guid CreatedBy { get; init; }
}
