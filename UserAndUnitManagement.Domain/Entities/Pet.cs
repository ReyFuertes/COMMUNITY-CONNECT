namespace UserAndUnitManagement.Domain.Entities
{
    public class Pet
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public required string Name { get; set; }
        public required string Breed { get; set; }
        public required string Species { get; set; } // e.g., Dog, Cat, Bird
        public required string PhotoUrl { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}