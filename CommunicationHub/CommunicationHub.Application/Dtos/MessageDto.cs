namespace CommunicationHub.Application.Dtos
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public Guid SenderId { get; set; }
        public required string Content { get; set; }
        public DateTime SentDate { get; set; }
    }
}
