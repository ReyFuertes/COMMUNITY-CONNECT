using AutoMapper;
using MediatR;
using MaintenanceService.Domain.Entities;
using MaintenanceService.Domain.Interfaces;
using MaintenanceService.Application.Dtos;

namespace MaintenanceService.Application.Features.WorkOrders.Commands.AddWorkOrderNote;

public class AddWorkOrderNoteHandler : IRequestHandler<AddWorkOrderNoteCommand, WorkOrderDto>
{
    private readonly IRepository<WorkOrder> _workOrderRepository;
    private readonly IRepository<WorkOrderNote> _workOrderNoteRepository;
    private readonly IMapper _mapper;

    public AddWorkOrderNoteHandler(IRepository<WorkOrder> workOrderRepository, IRepository<WorkOrderNote> workOrderNoteRepository, IMapper mapper)
    {
        _workOrderRepository = workOrderRepository;
        _workOrderNoteRepository = workOrderNoteRepository;
        _mapper = mapper;
    }

    public async Task<WorkOrderDto> Handle(AddWorkOrderNoteCommand request, CancellationToken cancellationToken)
    {
        var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);

        if (workOrder == null)
        {
            // TODO: Throw a specific NotFoundException
            throw new Exception($"WorkOrder with ID {request.WorkOrderId} not found.");
        }

        var note = new WorkOrderNote
        {
            Id = Guid.NewGuid(),
            WorkOrderId = request.WorkOrderId,
            Content = request.Content,
            CreatedBy = request.CreatedBy,
            CreatedDate = DateTime.UtcNow // Handled by BaseEntity, but can set explicitly
        };

        await _workOrderNoteRepository.AddAsync(note);
        await _workOrderNoteRepository.SaveChangesAsync();

        // No need to re-fetch with includes as IRepository doesn't directly support it without leaking EF Core.
        // The returned DTO will reflect the work order itself; the newly added note will be persisted
        // but not automatically included in this DTO unless explicitly requested later.
        return _mapper.Map<WorkOrderDto>(workOrder);
    }
}
