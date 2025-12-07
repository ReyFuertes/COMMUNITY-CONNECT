using CommunicationHub.Domain.Enums;

namespace CommunicationHub.Domain.Entities
{
    public class EmergencyAlert
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public EmergencyAlertType Type { get; set; }
        public DateTime SentDate { get; set; }
        public Guid AuthorId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ResolvedDate { get; set; }
    }
}
