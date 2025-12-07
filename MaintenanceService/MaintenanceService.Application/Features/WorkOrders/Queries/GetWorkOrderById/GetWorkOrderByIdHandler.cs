using AutoMapper;
using MediatR;
using MaintenanceService.Domain.Entities;
using MaintenanceService.Domain.Interfaces; // Use Domain IRepository
using MaintenanceService.Application.Dtos;
// Removed using Microsoft.EntityFrameworkCore;
// Removed using MaintenanceService.Application.Interfaces;

namespace MaintenanceService.Application.Features.WorkOrders.Queries.GetWorkOrderById;

public class GetWorkOrderByIdHandler : IRequestHandler<GetWorkOrderByIdQuery, WorkOrderDto>
{
    private readonly IRepository<WorkOrder> _workOrderRepository; // Reverted to IRepository
    private readonly IMapper _mapper;

    public GetWorkOrderByIdHandler(IRepository<WorkOrder> workOrderRepository, IMapper mapper) // Reverted to IRepository
    {
        _workOrderRepository = workOrderRepository;
        _mapper = mapper;
    }

    public async Task<WorkOrderDto> Handle(GetWorkOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var workOrder = await _workOrderRepository.GetByIdAsync(request.Id);

        if (workOrder == null)
        {
            // TODO: Throw a specific NotFoundException
            throw new Exception($"WorkOrder with ID {request.Id} not found.");
        }
        
        // Removed include logic (related entities will not be loaded)
        // If IncludeAttachments or IncludeNotes are true, they are currently ignored here
        // To include them, a specification pattern or explicit loading in infrastructure is needed.

        return _mapper.Map<WorkOrderDto>(workOrder);
    }
}
