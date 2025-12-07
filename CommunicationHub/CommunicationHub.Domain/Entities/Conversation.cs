namespace CommunicationHub.Domain.Entities
{
    public class Conversation
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } // Nullable for direct messages
        public bool IsGroup { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastMessageDate { get; set; }

        public ICollection<ConversationParticipant>? Participants { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
