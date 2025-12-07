using FluentValidation;

namespace MaintenanceService.Application.Features.WorkOrders.Commands.AddWorkOrderNote;

public class AddWorkOrderNoteCommandValidator : AbstractValidator<AddWorkOrderNoteCommand>
{
    public AddWorkOrderNoteCommandValidator()
    {
        RuleFor(x => x.WorkOrderId)
            .NotEmpty().WithMessage("Work Order ID is required.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Note content is required.")
            .MaximumLength(1000).WithMessage("Note content cannot exceed 1000 characters.");

        RuleFor(x => x.CreatedBy)
            .NotEmpty().WithMessage("Creator ID is required.");
    }
}
