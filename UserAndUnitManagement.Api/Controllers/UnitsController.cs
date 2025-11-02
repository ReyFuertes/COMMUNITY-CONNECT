using Microsoft.AspNetCore.Mvc;
using UserAndUnitManagement.Domain.Entities;
using UserAndUnitManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using UserAndUnitManagement.Application.Features.Units.Queries;
using UserAndUnitManagement.Application.Features.Units.Commands;
using Unit = MediatR.Unit;

namespace UserAndUnitManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UnitsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Units
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
            var units = await _mediator.Send(new GetAllUnitsQuery());
            return Ok(units);
        }

        // GET: api/Units/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(Guid id)
        {
            var unit = await _mediator.Send(new GetUnitByIdQuery { Id = id });
            if (unit == null)
            {
                return NotFound();
            }
            return Ok(unit);
        }

        // POST: api/Units
        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(CreateUnitCommand command)
        {
            var unit = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUnit), new { id = unit.Id }, unit);
        }

        // PUT: api/Units/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit(Guid id, UpdateUnitCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/Units/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(Guid id)
        {
            await _mediator.Send(new DeleteUnitCommand { Id = id });
            return NoContent();
        }
    }
}
