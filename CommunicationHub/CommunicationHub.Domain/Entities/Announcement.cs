using CommunicationHub.Domain.Enums;

namespace CommunicationHub.Domain.Entities
{
    public class Announcement
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public AnnouncementCategory Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public Guid AuthorId { get; set; } // Admin who created it
        
        public ICollection<AnnouncementAttachment>? Attachments { get; set; }
    }
}
