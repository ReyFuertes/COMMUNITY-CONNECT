using BookingService.Domain.Enums;

namespace BookingService.Application.Dtos
{
    public class BookingRuleDto
    {
        public Guid Id { get; set; }
        public Guid AmenityId { get; set; }
        public BookingRuleType RuleType { get; set; }
        public required string Value { get; set; }
        public DateTime? ApplicableFrom { get; set; }
        public DateTime? ApplicableTo { get; set; }
        public bool IsActive { get; set; }
    }
}