namespace BookingService.Application.Interfaces
{
    public interface IFinanceIntegrationClient
    {
        Task<string?> InitiateAmenityBookingPaymentAsync(Guid residentId, Guid bookingId, decimal amount, string description);
        Task<bool> RefundPaymentAsync(string transactionId);
    }
}