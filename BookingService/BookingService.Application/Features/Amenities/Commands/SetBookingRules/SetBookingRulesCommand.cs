using MediatR;
using BookingService.Application.Dtos;

namespace BookingService.Application.Features.Amenities.Commands.SetBookingRules
{
    public record SetBookingRulesCommand(Guid AmenityId, List<BookingRuleDto> Rules) : IRequest<bool>;
}