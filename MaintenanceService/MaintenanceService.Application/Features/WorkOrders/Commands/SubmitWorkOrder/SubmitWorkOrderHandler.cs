using AutoMapper;
using MediatR;
using MaintenanceService.Domain.Entities;
using MaintenanceService.Domain.Interfaces;
using MaintenanceService.Application.Dtos;
using MaintenanceService.Domain.Enums;

namespace MaintenanceService.Application.Features.WorkOrders.Commands.SubmitWorkOrder;

public class SubmitWorkOrderHandler : IRequestHandler<SubmitWorkOrderCommand, WorkOrderDto>
{
    private readonly IRepository<WorkOrder> _workOrderRepository;
    private readonly IMapper _mapper;

    public SubmitWorkOrderHandler(IRepository<WorkOrder> workOrderRepository, IMapper mapper)
    {
        _workOrderRepository = workOrderRepository;
        _mapper = mapper;
    }

    public async Task<WorkOrderDto> Handle(SubmitWorkOrderCommand request, CancellationToken cancellationToken)
    {
        var workOrder = new WorkOrder
        {
            Id = Guid.NewGuid(),
            Description = request.Description,
            Category = request.Category,
            UrgencyLevel = request.UrgencyLevel,
            UnitId = request.UnitId,
            RequesterId = request.RequesterId,
            Status = WorkOrderStatus.Submitted, // Initial status
            CreatedDate = DateTime.UtcNow
        };

        await _workOrderRepository.AddAsync(workOrder);
        await _workOrderRepository.SaveChangesAsync();

        return _mapper.Map<WorkOrderDto>(workOrder);
    }
}
