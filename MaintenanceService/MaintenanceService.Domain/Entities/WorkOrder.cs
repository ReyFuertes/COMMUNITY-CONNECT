using MaintenanceService.Domain.Enums;
using MaintenanceService.Domain.Entities;

namespace MaintenanceService.Domain.Entities;

public class WorkOrder : BaseEntity
{
    public required string Description { get; set; }
    public WorkOrderCategory Category { get; set; }
    public WorkOrderUrgency UrgencyLevel { get; set; }
    public WorkOrderStatus Status { get; set; }

    public Guid UnitId { get; set; } // Represents the unit the work order is for
    public Guid RequesterId { get; set; } // Represents the user who created the work order
    public Guid? AssignedToId { get; set; } // Represents the maintenance staff assigned

    public ICollection<WorkOrderAttachment> Attachments { get; set; } = new List<WorkOrderAttachment>();
    public ICollection<WorkOrderNote> Notes { get; set; } = new List<WorkOrderNote>();
}