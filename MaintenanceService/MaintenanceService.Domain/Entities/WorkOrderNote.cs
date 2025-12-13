using MaintenanceService.Domain.Entities;

namespace MaintenanceService.Domain.Entities;

public class WorkOrderNote : BaseEntity
{
    public Guid WorkOrderId { get; set; }
    public virtual WorkOrder? WorkOrder { get; set; } // Navigation property
    public required string Content { get; set; }
    public Guid CreatedBy { get; set; } // User ID of the person who created the note
}