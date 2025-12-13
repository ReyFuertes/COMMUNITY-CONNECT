using BookingService.Domain.Enums;

namespace BookingService.Domain.Entities
{
    public class BookingRule
    {
        public Guid Id { get; set; }
        public Guid AmenityId { get; set; }
        public Amenity? Amenity { get; set; } // Navigation property
        public BookingRuleType RuleType { get; set; }
        public required string Value { get; set; } // e.g., "2 hours", "10 people", "2025-12-25"
        public DateTime? ApplicableFrom { get; set; }
        public DateTime? ApplicableTo { get; set; }
        public bool IsActive { get; set; } = true;
    }
}