using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace CommunicationHub.Client.Services
{
    public class SignalRCommunicationClient : IAsyncDisposable
    {
        private HubConnection? _connection; // Made nullable
        private readonly ILogger<SignalRCommunicationClient> _logger;

        public SignalRCommunicationClient(ILogger<SignalRCommunicationClient> logger)
        {
            _logger = logger;
        }

        // Configuration property for the Hub URL
        public string HubUrl { get; set; } = "http://localhost:8080/hubs/communication";
        public Func<Task<string?>>? AccessTokenProvider { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(HubUrl, options =>
                {
                    if (AccessTokenProvider != null)
                    {
                        options.AccessTokenProvider = AccessTokenProvider;
                    }
                })
                .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(10) })
                .Build();

            // Subscribe to methods that the server hub pushes
            _connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                _logger.LogInformation("Received from hub - User: {User}, Message: {Message}", user, message);
                // TODO: forward to your own clients, save to DB, etc.
            });

            _connection.On<string>("Broadcast", (payload) =>
            {
                _logger.LogInformation("Broadcast received: {Payload}", payload);
            });
            
            // Add other event handlers for AnnouncementDto, EmergencyAlertDto
            _connection.On<object>("ReceiveAnnouncement", (announcement) => // Use object for now, replace with actual DTO once they are in common lib
            {
                _logger.LogInformation("Received Announcement: {Announcement}", announcement.ToString());
            });

            _connection.On<object>("ReceiveEmergencyAlert", (alert) => // Use object for now, replace with actual DTO
            {
                _logger.LogInformation("Received Emergency Alert: {Alert}", alert.ToString());
            });

            try
            {
                await _connection.StartAsync(cancellationToken);
                _logger.LogInformation("SignalR client connected to communication hub at {HubUrl}", HubUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to SignalR hub at {HubUrl}", HubUrl);
                throw;
            }
        }
        
        public async Task StopAsync()
        {
            if (_connection?.State == HubConnectionState.Connected)
            {
                await _connection.StopAsync();
                _logger.LogInformation("SignalR client disconnected.");
            }
        }

        // Call server methods if needed
        public async Task SendMessageAsync(string user, string message)
        {
            if (_connection?.State == HubConnectionState.Connected)
            {
                await _connection.InvokeAsync("SendMessage", user, message);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }
        }
    }
}
