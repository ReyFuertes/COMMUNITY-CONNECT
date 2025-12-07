using CommunicationHub.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CommunicationHub.Client; // Explicitly add this using directive

Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddEnvironmentVariables();
        if (hostingContext.HostingEnvironment.IsDevelopment())
        {
            config.AddUserSecrets<Program>();
        }
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        // Register the SignalRCommunicationClient
        services.AddSingleton<SignalRCommunicationClient>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<SignalRCommunicationClient>>();
            var client = new SignalRCommunicationClient(logger);
            
            // Get HubUrl from configuration
            client.HubUrl = hostContext.Configuration.GetValue<string>("SignalRClient:HubUrl") ?? "http://localhost:8080/hubs/communication";

            // If authentication is needed
            var jwtToken = hostContext.Configuration.GetValue<string>("JwtSettings:Token");
            if (!string.IsNullOrEmpty(jwtToken))
            {
                client.AccessTokenProvider = () => Task.FromResult<string?>(jwtToken);
            }

            return client;
        });

        // Register the hosted service to start and stop the client
        services.AddHostedService<SignalRHostedService>();
    })
    .Build()
    .Run();