using MediatR;
using MaintenanceService.Application.Dtos;

namespace MaintenanceService.Application.Features.WorkOrders.Queries.GetWorkOrderById;

public record GetWorkOrderByIdQuery : IRequest<WorkOrderDto>
{
    public Guid Id { get; init; }
    public bool IncludeAttachments { get; set; } = false;
    public bool IncludeNotes { get; set; } = false;
}
