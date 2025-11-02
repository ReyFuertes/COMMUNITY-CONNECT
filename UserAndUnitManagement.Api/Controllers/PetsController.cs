using Microsoft.AspNetCore.Mvc;
using UserAndUnitManagement.Domain.Entities;
using System.Threading.Tasks;
using MediatR;
using UserAndUnitManagement.Application.Features.Pets.Commands;
using UserAndUnitManagement.Application.Features.Pets.Queries;
using System.Collections.Generic;

namespace UserAndUnitManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Pets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPets()
        {
            var pets = await _mediator.Send(new GetAllPetsQuery());
            return Ok(pets);
        }

        // GET: api/Pets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPet(Guid id)
        {
            var pet = await _mediator.Send(new GetPetByIdQuery { Id = id });
            if (pet == null)
            {
                return NotFound();
            }
            return Ok(pet);
        }

        // POST: api/Pets
        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(CreatePetCommand command)
        {
            var pet = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPet), new { id = pet.Id }, pet);
        }

        // PUT: api/Pets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(Guid id, UpdatePetCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(Guid id)
        {
            await _mediator.Send(new DeletePetCommand { Id = id });
            return NoContent();
        }
    }
}
