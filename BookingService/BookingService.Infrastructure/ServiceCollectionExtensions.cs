using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookingService.Application.Interfaces;
using BookingService.Domain.Interfaces;
using BookingService.Infrastructure.Persistence;
using BookingService.Infrastructure.Repositories;
using BookingService.Infrastructure.Services;

namespace BookingService.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookingDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(BookingDbContext).Assembly.FullName)));

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddHttpClient<IFinanceIntegrationClient, FinanceIntegrationClient>();
            services.AddHttpClient<INotificationClient, NotificationClient>();
            services.AddHttpClient<IUserAndUnitIntegrationClient, UserAndUnitIntegrationClient>();

            return services;
        }
    }
}