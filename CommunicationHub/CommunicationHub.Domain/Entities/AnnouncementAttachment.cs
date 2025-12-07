namespace CommunicationHub.Domain.Entities
{
    public class AnnouncementAttachment
    {
        public Guid Id { get; set; }
        public Guid AnnouncementId { get; set; }
        public required string FileName { get; set; }
        public required string FileUrl { get; set; }
        public DateTime UploadedDate { get; set; }

        public Announcement? Announcement { get; set; }
    }
}
