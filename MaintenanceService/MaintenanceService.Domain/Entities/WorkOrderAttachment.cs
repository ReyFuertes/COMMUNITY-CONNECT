using MaintenanceService.Domain.Entities;

namespace MaintenanceService.Domain.Entities;

public class WorkOrderAttachment : BaseEntity
{
    public Guid WorkOrderId { get; set; }
    public virtual WorkOrder? WorkOrder { get; set; } // Navigation property
    public required string FilePath { get; set; }
    public required string FileName { get; set; }
}