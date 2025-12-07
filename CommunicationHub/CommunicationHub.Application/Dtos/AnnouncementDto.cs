namespace CommunicationHub.Application.Dtos
{
    public class AnnouncementDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Category { get; set; } // Enum as string
        public DateTime CreatedDate { get; set; }
    }
}
