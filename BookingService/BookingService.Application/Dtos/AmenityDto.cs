namespace BookingService.Application.Dtos
{
    public class AmenityDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public decimal BookingFee { get; set; }
        public decimal SecurityDeposit { get; set; }
        public bool RequiresApproval { get; set; }
        public bool IsActive { get; set; }
    }
}