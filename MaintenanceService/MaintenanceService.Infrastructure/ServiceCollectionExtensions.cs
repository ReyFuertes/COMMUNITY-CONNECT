using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MaintenanceService.Domain.Interfaces;
using MaintenanceService.Infrastructure.Persistence;
using MaintenanceService.Infrastructure.Repositories;
// Removed using MaintenanceService.Application.Interfaces;

namespace MaintenanceService.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<MaintenanceDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        // Removed services.AddScoped<IWorkOrderRepository, WorkOrderRepository>(); // Register specific work order repository

        return services;
    }
}
