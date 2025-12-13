namespace BookingService.Domain.Entities
{
    public class Amenity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public decimal BookingFee { get; set; }
        public decimal SecurityDeposit { get; set; }
        public bool RequiresApproval { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<BookingRule> Rules { get; set; } = new List<BookingRule>();
    }
}