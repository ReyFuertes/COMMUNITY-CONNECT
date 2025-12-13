namespace BookingService.Application.Interfaces
{
    public interface INotificationClient
    {
        Task SendBookingConfirmationAsync(Guid residentId, string amenityName, DateTime startTime);
        Task SendBookingStatusUpdateAsync(Guid residentId, string amenityName, BookingService.Domain.Enums.BookingStatus newStatus);
    }
}