using MaintenanceService.Domain.Enums;

namespace MaintenanceService.Application.Dtos;

public class WorkOrderDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public WorkOrderCategory Category { get; set; }
    public WorkOrderUrgency UrgencyLevel { get; set; }
    public WorkOrderStatus Status { get; set; }
    public Guid UnitId { get; set; }
    public Guid RequesterId { get; set; }
    public Guid? AssignedToId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}
