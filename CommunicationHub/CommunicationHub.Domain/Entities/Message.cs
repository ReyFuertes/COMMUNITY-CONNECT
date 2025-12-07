namespace CommunicationHub.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public Guid SenderId { get; set; }
        public required string Content { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }

        public Conversation? Conversation { get; set; }
    }
}
