namespace CommunicationHub.Application.Dtos
{
    public class EmergencyAlertDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Type { get; set; } // Enum as string
        public DateTime SentDate { get; set; }
    }
}
