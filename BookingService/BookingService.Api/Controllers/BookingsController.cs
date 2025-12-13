using MediatR;
using Microsoft.AspNetCore.Mvc;
using BookingService.Application.Features.Bookings.Commands.ApproveBooking;
using BookingService.Application.Features.Bookings.Commands.CreateBooking;
using BookingService.Application.Features.Bookings.Queries.GetAmenityAvailability;
using BookingService.Application.Features.Bookings.Queries.GetResidentBookings;

namespace BookingService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBooking([FromBody] CreateBookingCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetResidentBookings), new { residentId = command.ResidentId }, result);
        }

        [HttpPost("{id}/approve")]
        public async Task<ActionResult> ApproveBooking(Guid id, [FromQuery] Guid adminId, [FromQuery] bool approve)
        {
            var result = await _mediator.Send(new ApproveBookingCommand(id, adminId, approve));
            if (!result) return NotFound("Booking not found or not in pending state.");
            return Ok(result);
        }

        [HttpGet("resident/{residentId}")]
        public async Task<ActionResult> GetResidentBookings(Guid residentId)
        {
            var result = await _mediator.Send(new GetResidentBookingsQuery(residentId));
            return Ok(result);
        }

        [HttpGet("{amenityId}/availability")]
        public async Task<ActionResult> GetAmenityAvailability(Guid amenityId, [FromQuery] DateTime date)
        {
            var result = await _mediator.Send(new GetAmenityAvailabilityQuery(amenityId, date));
            return Ok(result);
        }
    }
}
