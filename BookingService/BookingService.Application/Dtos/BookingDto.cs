using BookingService.Domain.Enums;

namespace BookingService.Application.Dtos
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public Guid AmenityId { get; set; }
        public required string AmenityName { get; set; }
        public Guid ResidentId { get; set; }
        public Guid UnitId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public BookingStatus Status { get; set; }
        public string? PaymentTransactionId { get; set; }
    }
}