using BookingService.Domain.Enums;

namespace BookingService.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid AmenityId { get; set; }
        public Amenity? Amenity { get; set; } // Navigation property
        public Guid ResidentId { get; set; }
        public Guid UnitId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public string? PaymentTransactionId { get; set; } // Link to FinanceService
        public string? AdminNotes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ApprovedRejectedAt { get; set; }
        public Guid? ApprovedRejectedBy { get; set; } // Admin User ID
    }
}