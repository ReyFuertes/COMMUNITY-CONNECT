using AutoMapper;
using MediatR;
using MaintenanceService.Domain.Entities;
using MaintenanceService.Domain.Interfaces;
using MaintenanceService.Application.Dtos;

namespace MaintenanceService.Application.Features.WorkOrders.Commands.AssignWorkOrder;

public class AssignWorkOrderHandler : IRequestHandler<AssignWorkOrderCommand, WorkOrderDto>
{
    private readonly IRepository<WorkOrder> _workOrderRepository;
    private readonly IMapper _mapper;

    public AssignWorkOrderHandler(IRepository<WorkOrder> workOrderRepository, IMapper mapper)
    {
        _workOrderRepository = workOrderRepository;
        _mapper = mapper;
    }

    public async Task<WorkOrderDto> Handle(AssignWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);

        if (workOrder == null)
        {
            // TODO: Throw a specific NotFoundException
            throw new Exception($"WorkOrder with ID {request.WorkOrderId} not found.");
        }

        workOrder.AssignedToId = request.AssignedToId;
        workOrder.LastModifiedDate = DateTime.UtcNow; // Handled by DbContext override, but good to set explicitly

        _workOrderRepository.Update(workOrder);
        await _workOrderRepository.SaveChangesAsync();

        return _mapper.Map<WorkOrderDto>(workOrder);
    }
}
