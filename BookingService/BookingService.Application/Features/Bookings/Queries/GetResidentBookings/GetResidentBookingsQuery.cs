using MediatR;
using BookingService.Application.Dtos;

namespace BookingService.Application.Features.Bookings.Queries.GetResidentBookings
{
    public record GetResidentBookingsQuery(Guid ResidentId) : IRequest<List<BookingDto>>;
}