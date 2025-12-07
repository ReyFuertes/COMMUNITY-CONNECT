using FluentValidation;
using MaintenanceService.Domain.Enums;

namespace MaintenanceService.Application.Features.WorkOrders.Commands.SubmitWorkOrder;

public class SubmitWorkOrderCommandValidator : AbstractValidator<SubmitWorkOrderCommand>
{
    public SubmitWorkOrderCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Invalid work order category.");

        RuleFor(x => x.UrgencyLevel)
            .IsInEnum().WithMessage("Invalid urgency level.");

        RuleFor(x => x.UnitId)
            .NotEmpty().WithMessage("Unit ID is required.");

        RuleFor(x => x.RequesterId)
            .NotEmpty().WithMessage("Requester ID is required.");
    }
}
