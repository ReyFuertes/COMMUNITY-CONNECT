using MaintenanceService.Domain.Entities;

namespace MaintenanceService.Domain.Entities;

public class WorkOrderAttachment : BaseEntity
{
    public Guid WorkOrderId { get; set; }
    public WorkOrder WorkOrder { get; set; } // Navigation property
    public string FilePath { get; set; }
    public string FileName { get; set; }
}
