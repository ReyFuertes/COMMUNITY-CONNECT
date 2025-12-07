namespace CommunicationHub.Domain.Entities
{
    public class ConversationParticipant
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; } // If group chat admin
        public DateTime JoinedDate { get; set; }

        public Conversation? Conversation { get; set; }
    }
}
