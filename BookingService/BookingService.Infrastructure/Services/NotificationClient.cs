using BookingService.Application.Interfaces;
using BookingService.Domain.Enums;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Services
{
    public class NotificationClient : INotificationClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _communicationHubBaseUrl;

        public NotificationClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _communicationHubBaseUrl = configuration["CommunicationHub:BaseUrl"] ?? throw new ArgumentNullException("CommunicationHub:BaseUrl configuration is missing");
            _httpClient.BaseAddress = new Uri(_communicationHubBaseUrl);
        }

        public async Task SendBookingConfirmationAsync(Guid residentId, string amenityName, DateTime startTime)
        {
            // Assuming CommunicationHub has a generic notification endpoint
            // In a real scenario, this would likely be a more specific endpoint
            var notification = new
            {
                RecipientId = residentId,
                Message = $"Your booking for {amenityName} on {startTime:yyyy-MM-dd HH:mm} is confirmed.",
                Type = "BookingConfirmation"
            };
            // await _httpClient.PostAsJsonAsync("/api/communication/notifications", notification); // Assuming a notifications controller
            await Task.CompletedTask; // Placeholder for actual HTTP call
        }

        public async Task SendBookingStatusUpdateAsync(Guid residentId, string amenityName, BookingStatus newStatus)
        {
            var notification = new
            {
                RecipientId = residentId,
                Message = $"Your booking for {amenityName} is now: {newStatus}.",
                Type = "BookingStatusUpdate"
            };
            // await _httpClient.PostAsJsonAsync("/api/communication/notifications", notification); // Assuming a notifications controller
            await Task.CompletedTask; // Placeholder for actual HTTP call
        }
    }
}