using MediatR;
using BookingService.Application.Dtos;

namespace BookingService.Application.Features.Amenities.Commands.CreateAmenity
{
    public record CreateAmenityCommand(
        string Name, 
        string? Description, 
        int Capacity, 
        decimal BookingFee, 
        decimal SecurityDeposit, 
        bool RequiresApproval, 
        bool IsActive) : IRequest<AmenityDto>;
}