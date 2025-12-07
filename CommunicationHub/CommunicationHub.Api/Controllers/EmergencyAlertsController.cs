using CommunicationHub.Application.Features.EmergencyAlerts.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmergencyAlertsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmergencyAlertsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmergencyAlertCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
