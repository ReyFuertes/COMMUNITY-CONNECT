using MediatR;

namespace BookingService.Application.Features.Bookings.Commands.ApproveBooking
{
    public record ApproveBookingCommand(Guid BookingId, Guid AdminId, bool Approve) : IRequest<bool>;
}