using MediatR;
using BookingService.Application.Interfaces;
using BookingService.Domain.Enums;
using BookingService.Domain.Interfaces;

namespace BookingService.Application.Features.Bookings.Commands.ApproveBooking
{
    public class ApproveBookingHandler : IRequestHandler<ApproveBookingCommand, bool>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly INotificationClient _notificationClient;

        public ApproveBookingHandler(IBookingRepository bookingRepository, INotificationClient notificationClient)
        {
            _bookingRepository = bookingRepository;
            _notificationClient = notificationClient;
        }

        public async Task<bool> Handle(ApproveBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null) return false;

            if (booking.Status != BookingStatus.Pending) return false; // Only pending bookings can be approved/rejected

            booking.Status = request.Approve ? BookingStatus.Approved : BookingStatus.Rejected;
            booking.ApprovedRejectedAt = DateTime.UtcNow;
            booking.ApprovedRejectedBy = request.AdminId;

            await _bookingRepository.UpdateAsync(booking);

            // Notify resident of status change
            await _notificationClient.SendBookingStatusUpdateAsync(booking.ResidentId, booking.Amenity?.Name ?? "Amenity", booking.Status);

            return true;
        }
    }
}