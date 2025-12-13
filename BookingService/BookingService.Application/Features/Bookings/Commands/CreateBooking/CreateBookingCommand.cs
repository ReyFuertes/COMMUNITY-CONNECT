using MediatR;
using BookingService.Application.Dtos;

namespace BookingService.Application.Features.Bookings.Commands.CreateBooking
{
    public record CreateBookingCommand(Guid AmenityId, Guid ResidentId, Guid UnitId, DateTime StartTime, DateTime EndTime) : IRequest<BookingDto>;
}