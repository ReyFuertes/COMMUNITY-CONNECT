using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CommunicationHub.Client.Services;

namespace CommunicationHub.Client
{
    public class SignalRHostedService : IHostedService
    {
        private readonly SignalRCommunicationClient _client;
        private readonly ILogger<SignalRHostedService> _logger;

        public SignalRHostedService(SignalRCommunicationClient client, ILogger<SignalRHostedService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting SignalR client...");
            await _client.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping SignalR client...");
            await _client.StopAsync(); // Ensure the client's custom StopAsync is called
        }
    }
}
