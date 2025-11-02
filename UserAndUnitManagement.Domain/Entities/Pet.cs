namespace UserAndUnitManagement.Domain.Entities
{
    public class Pet
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Species { get; set; } // e.g., Dog, Cat, Bird
        public string PhotoUrl { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}