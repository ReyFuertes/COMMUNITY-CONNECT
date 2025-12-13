using BookingService.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Services
{
    public class UserAndUnitIntegrationClient : IUserAndUnitIntegrationClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _userAndUnitServiceBaseUrl;

        public UserAndUnitIntegrationClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _userAndUnitServiceBaseUrl = configuration["UserAndUnitManagement:BaseUrl"] ?? throw new ArgumentNullException("UserAndUnitManagement:BaseUrl configuration is missing");
            _httpClient.BaseAddress = new Uri(_userAndUnitServiceBaseUrl);
        }

        public async Task<bool> ResidentExistsAsync(Guid residentId)
        {
            // Assuming UserAndUnitManagement has an endpoint like /api/users/{residentId}/exists
            // Or we can try to fetch the user and check for null
            var response = await _httpClient.GetAsync($"/api/users/{residentId}/exists");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UnitExistsAsync(Guid unitId)
        {
            // Assuming UserAndUnitManagement has an endpoint like /api/units/{unitId}/exists
            // Or we can try to fetch the unit and check for null
            var response = await _httpClient.GetAsync($"/api/units/{unitId}/exists");
            return response.IsSuccessStatusCode;
        }
    }
}