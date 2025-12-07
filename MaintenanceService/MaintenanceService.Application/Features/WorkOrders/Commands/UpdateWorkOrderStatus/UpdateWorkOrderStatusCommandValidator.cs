using FluentValidation;
using MaintenanceService.Domain.Enums;

namespace MaintenanceService.Application.Features.WorkOrders.Commands.UpdateWorkOrderStatus;

public class UpdateWorkOrderStatusCommandValidator : AbstractValidator<UpdateWorkOrderStatusCommand>
{
    public UpdateWorkOrderStatusCommandValidator()
    {
        RuleFor(x => x.WorkOrderId)
            .NotEmpty().WithMessage("Work Order ID is required.");

        RuleFor(x => x.NewStatus)
            .IsInEnum().WithMessage("Invalid work order status.");

        // Additional validation for status transitions could be placed here
        // or handled within the business logic of the handler.
    }
}
