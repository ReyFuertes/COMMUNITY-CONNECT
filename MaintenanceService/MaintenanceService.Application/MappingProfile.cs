using AutoMapper;
using MaintenanceService.Application.Dtos;
using MaintenanceService.Domain.Entities;

namespace MaintenanceService.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WorkOrder, WorkOrderDto>().ReverseMap();
        // Add other mappings here as needed
    }
}
