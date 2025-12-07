using AutoMapper;
using MediatR;
using MaintenanceService.Domain.Entities;
using MaintenanceService.Domain.Interfaces; // Use Domain IRepository
using MaintenanceService.Application.Dtos;
// Removed using Microsoft.EntityFrameworkCore;
// Removed using MaintenanceService.Application.Interfaces;

namespace MaintenanceService.Application.Features.WorkOrders.Queries.GetWorkOrdersByUnit;

public class GetWorkOrdersByUnitHandler : IRequestHandler<GetWorkOrdersByUnitQuery, IEnumerable<WorkOrderDto>>
{
    private readonly IRepository<WorkOrder> _workOrderRepository; // Reverted to IRepository
    private readonly IMapper _mapper;

    public GetWorkOrdersByUnitHandler(IRepository<WorkOrder> workOrderRepository, IMapper mapper) // Reverted to IRepository
    {
        _workOrderRepository = workOrderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WorkOrderDto>> Handle(GetWorkOrdersByUnitQuery request, CancellationToken cancellationToken)
    {
        var workOrders = await _workOrderRepository.FindAsync( // Changed to FindAsync
            predicate: wo => wo.UnitId == request.UnitId
            // Removed include and orderBy parameters
        );

        return _mapper.Map<IEnumerable<WorkOrderDto>>(workOrders);
    }
}
