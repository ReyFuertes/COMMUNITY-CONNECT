namespace UserAndUnitManagement.Domain.Entities
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public required string PlateNumber { get; set; }
        public required string Color { get; set; }
        public int Year { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}