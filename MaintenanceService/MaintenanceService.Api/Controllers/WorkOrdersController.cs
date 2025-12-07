using MediatR;
using Microsoft.AspNetCore.Mvc;
using MaintenanceService.Application.Features.WorkOrders.Commands.SubmitWorkOrder;
using MaintenanceService.Application.Dtos;
using MaintenanceService.Application.Features.WorkOrders.Queries.GetWorkOrders;
using MaintenanceService.Domain.Enums;
using MaintenanceService.Application.Features.WorkOrders.Commands.UpdateWorkOrderStatus;
using MaintenanceService.Application.Features.WorkOrders.Commands.AssignWorkOrder;
using MaintenanceService.Application.Features.WorkOrders.Commands.AddWorkOrderNote;
using MaintenanceService.Application.Features.WorkOrders.Queries.GetWorkOrderById;
using MaintenanceService.Application.Features.WorkOrders.Queries.GetWorkOrdersByUnit;

namespace MaintenanceService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkOrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkOrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SubmitWorkOrder([FromBody] SubmitWorkOrderCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var workOrderDto = await _mediator.Send(command);
        return Ok(workOrderDto);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WorkOrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWorkOrders(
        [FromQuery] WorkOrderStatus? status,
        [FromQuery] WorkOrderUrgency? urgencyLevel,
        [FromQuery] WorkOrderCategory? category,
        [FromQuery] Guid? unitId,
        [FromQuery] Guid? requesterId,
        [FromQuery] Guid? assignedToId,
        [FromQuery] bool includeAttachments = false,
        [FromQuery] bool includeNotes = false)
    {
        var query = new GetWorkOrdersQuery
        {
            Status = status,
            UrgencyLevel = urgencyLevel,
            Category = category,
            UnitId = unitId,
            RequesterId = requesterId,
            AssignedToId = assignedToId,
            IncludeAttachments = includeAttachments,
            IncludeNotes = includeNotes
        };
        var workOrders = await _mediator.Send(query);
        return Ok(workOrders);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWorkOrderById(Guid id, [FromQuery] bool includeAttachments = false, [FromQuery] bool includeNotes = false)
    {
        var query = new GetWorkOrderByIdQuery { Id = id, IncludeAttachments = includeAttachments, IncludeNotes = includeNotes };
        try
        {
            var workOrder = await _mediator.Send(query);
            return Ok(workOrder);
        }
        catch (Exception ex) // TODO: Catch specific NotFoundException
        {
            if (ex.Message.Contains("not found"))
            {
                return NotFound(ex.Message);
            }
            throw; // Re-throw unhandled exceptions
        }
    }

    [HttpGet("~/api/units/{unitId}/workorders")] // New route for unit-specific work orders
    [ProducesResponseType(typeof(IEnumerable<WorkOrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWorkOrdersForUnit(Guid unitId, [FromQuery] bool includeAttachments = false, [FromQuery] bool includeNotes = false)
    {
        var query = new GetWorkOrdersByUnitQuery { UnitId = unitId, IncludeAttachments = includeAttachments, IncludeNotes = includeNotes };
        var workOrders = await _mediator.Send(query);
        // If an empty list is returned, it means no work orders for the unit, not that the unit was not found.
        // So, we just return Ok(workOrders).
        return Ok(workOrders);
    }

    [HttpPatch("{id}/status")]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateWorkOrderStatus(Guid id, [FromBody] UpdateWorkOrderStatusCommand command)
    {
        if (id != command.WorkOrderId)
        {
            return BadRequest("Work Order ID in URL must match Work Order ID in body.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var workOrderDto = await _mediator.Send(command);
            return Ok(workOrderDto);
        }
        catch (Exception ex) // TODO: Catch specific exceptions (e.g., NotFoundException, InvalidStatusTransitionException)
        {
            if (ex.Message.Contains("not found")) // Crude check for now
            {
                return NotFound(ex.Message);
            }
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}/assign")]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignWorkOrder(Guid id, [FromBody] AssignWorkOrderCommand command)
    {
        if (id != command.WorkOrderId)
        {
            return BadRequest("Work Order ID in URL must match Work Order ID in body.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var workOrderDto = await _mediator.Send(command);
            return Ok(workOrderDto);
        }
        catch (Exception ex) // TODO: Catch specific exceptions (e.g., NotFoundException)
        {
            if (ex.Message.Contains("not found")) // Crude check for now
            {
                return NotFound(ex.Message);
            }
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/notes")]
    [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddWorkOrderNote(Guid id, [FromBody] AddWorkOrderNoteCommand command)
    {
        if (id != command.WorkOrderId)
        {
            return BadRequest("Work Order ID in URL must match Work Order ID in body.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var workOrderDto = await _mediator.Send(command);
            return Ok(workOrderDto);
        }
        catch (Exception ex) // TODO: Catch specific exceptions (e.g., NotFoundException)
        {
            if (ex.Message.Contains("not found")) // Crude check for now
            {
                return NotFound(ex.Message);
            }
            return BadRequest(ex.Message);
        }
    }
}

