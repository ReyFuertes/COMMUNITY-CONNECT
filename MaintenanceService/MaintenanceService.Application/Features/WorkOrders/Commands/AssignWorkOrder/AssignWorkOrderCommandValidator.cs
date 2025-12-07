using FluentValidation;

namespace MaintenanceService.Application.Features.WorkOrders.Commands.AssignWorkOrder;

public class AssignWorkOrderCommandValidator : AbstractValidator<AssignWorkOrderCommand>
{
    public AssignWorkOrderCommandValidator()
    {
        RuleFor(x => x.WorkOrderId)
            .NotEmpty().WithMessage("Work Order ID is required.");
        
        // AssignedToId can be null, so no NotEmpty validation here
    }
}
