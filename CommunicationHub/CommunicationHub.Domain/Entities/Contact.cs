namespace CommunicationHub.Domain.Entities
{
    public class Contact
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public required string Role { get; set; } // e.g. "Security Desk", "Admin Office"
        public string? Description { get; set; }
        public bool IsEmergencyNumber { get; set; }
    }
}
