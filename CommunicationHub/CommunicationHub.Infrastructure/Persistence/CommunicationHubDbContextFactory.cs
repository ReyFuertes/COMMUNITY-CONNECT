using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CommunicationHub.Infrastructure.Persistence
{
    public class CommunicationHubDbContextFactory : IDesignTimeDbContextFactory<CommunicationHubDbContext>
    {
        public CommunicationHubDbContext CreateDbContext(string[] args)
        {
            // Build configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();

            // Create DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<CommunicationHubDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new CommunicationHubDbContext(builder.Options);
        }
    }
}
