using MediatR;
using Microsoft.AspNetCore.Mvc;
using BookingService.Application.Features.Amenities.Commands.CreateAmenity;
using BookingService.Application.Features.Amenities.Commands.SetBookingRules;

namespace BookingService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmenitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AmenitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAmenity([FromBody] CreateAmenityCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateAmenity), new { id = result.Id }, result);
        }

        [HttpPut("{id}/rules")]
        public async Task<ActionResult> SetBookingRules(Guid id, [FromBody] List<BookingService.Application.Dtos.BookingRuleDto> rules)
        {
            var result = await _mediator.Send(new SetBookingRulesCommand(id, rules));
            if (!result) return NotFound("Amenity not found.");
            return Ok();
        }
    }
}