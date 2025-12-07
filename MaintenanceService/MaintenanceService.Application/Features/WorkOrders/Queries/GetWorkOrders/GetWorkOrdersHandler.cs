using AutoMapper;
using MediatR;
using MaintenanceService.Domain.Entities;
using MaintenanceService.Domain.Interfaces; // Use Domain IRepository
using MaintenanceService.Application.Dtos;
using MaintenanceService.Domain.Enums;
// Removed using Microsoft.EntityFrameworkCore;
// Removed using MaintenanceService.Infrastructure.Interfaces;

namespace MaintenanceService.Application.Features.WorkOrders.Queries.GetWorkOrders;

public class GetWorkOrdersHandler : IRequestHandler<GetWorkOrdersQuery, IEnumerable<WorkOrderDto>>
{
    private readonly IRepository<WorkOrder> _workOrderRepository; // Reverted to IRepository
    private readonly IMapper _mapper;

    public GetWorkOrdersHandler(IRepository<WorkOrder> workOrderRepository, IMapper mapper) // Reverted to IRepository
    {
        _workOrderRepository = workOrderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WorkOrderDto>> Handle(GetWorkOrdersQuery request, CancellationToken cancellationToken)
    {
        var workOrders = await _workOrderRepository.FindAsync( // Changed to FindAsync
            predicate: wo =>
                (!request.Status.HasValue || wo.Status == request.Status.Value) &&
                (!request.UrgencyLevel.HasValue || wo.UrgencyLevel == request.UrgencyLevel.Value) &&
                (!request.Category.HasValue || wo.Category == request.Category.Value) &&
                (!request.UnitId.HasValue || wo.UnitId == request.UnitId.Value) &&
                (!request.RequesterId.HasValue || wo.RequesterId == request.RequesterId.Value) &&
                (!request.AssignedToId.HasValue || wo.AssignedToId == request.AssignedToId.Value)
            // Removed include and orderBy parameters
        );

        return _mapper.Map<IEnumerable<WorkOrderDto>>(workOrders);
    }
}
