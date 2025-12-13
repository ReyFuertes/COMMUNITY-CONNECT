using MediatR;
using BookingService.Application.Dtos;

namespace BookingService.Application.Features.Bookings.Queries.GetAmenityAvailability
{
    public record GetAmenityAvailabilityQuery(Guid AmenityId, DateTime Date) : IRequest<List<TimeSpan>>;
}