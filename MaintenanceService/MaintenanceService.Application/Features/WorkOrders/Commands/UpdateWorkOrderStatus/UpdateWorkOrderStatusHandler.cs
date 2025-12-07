using AutoMapper;
using MediatR;
using MaintenanceService.Domain.Entities;
using MaintenanceService.Domain.Interfaces;
using MaintenanceService.Application.Dtos;

namespace MaintenanceService.Application.Features.WorkOrders.Commands.UpdateWorkOrderStatus;

public class UpdateWorkOrderStatusHandler : IRequestHandler<UpdateWorkOrderStatusCommand, WorkOrderDto>
{
    private readonly IRepository<WorkOrder> _workOrderRepository;
    private readonly IMapper _mapper;

    public UpdateWorkOrderStatusHandler(IRepository<WorkOrder> workOrderRepository, IMapper mapper)
    {
        _workOrderRepository = workOrderRepository;
        _mapper = mapper;
    }

    public async Task<WorkOrderDto> Handle(UpdateWorkOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);

        if (workOrder == null)
        {
            // TODO: Throw a specific NotFoundException
            throw new Exception($"WorkOrder with ID {request.WorkOrderId} not found.");
        }

        // Basic status transition validation (can be more complex)
        if (workOrder.Status == request.NewStatus)
        {
            return _mapper.Map<WorkOrderDto>(workOrder); // No change needed
        }

        // Example of a simple state machine:
        // Submitted -> InProgress -> Completed
        // Submitted -> Cancelled
        // InProgress -> Completed
        // InProgress -> Cancelled
        // Completed -> (no change usually)
        // Cancelled -> (no change usually, or reopen)

        bool isValidTransition = (workOrder.Status, request.NewStatus) switch
        {
            (Domain.Enums.WorkOrderStatus.Submitted, Domain.Enums.WorkOrderStatus.InProgress) => true,
            (Domain.Enums.WorkOrderStatus.Submitted, Domain.Enums.WorkOrderStatus.Cancelled) => true,
            (Domain.Enums.WorkOrderStatus.InProgress, Domain.Enums.WorkOrderStatus.Completed) => true,
            (Domain.Enums.WorkOrderStatus.InProgress, Domain.Enums.WorkOrderStatus.Cancelled) => true,
            _ => false
        };

        if (!isValidTransition && workOrder.Status != request.NewStatus)
        {
             // TODO: Throw a specific InvalidStatusTransitionException
            throw new Exception($"Invalid status transition from {workOrder.Status} to {request.NewStatus}.");
        }


        workOrder.Status = request.NewStatus;
        workOrder.LastModifiedDate = DateTime.UtcNow; // Handled by DbContext override, but good to set explicitly
        // You might also want to track who updated it, using request.UpdatedById

        _workOrderRepository.Update(workOrder);
        await _workOrderRepository.SaveChangesAsync();

        return _mapper.Map<WorkOrderDto>(workOrder);
    }
}
