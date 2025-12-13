namespace BookingService.Application.Interfaces
{
    public interface IUserAndUnitIntegrationClient
    {
        Task<bool> ResidentExistsAsync(Guid residentId);
        Task<bool> UnitExistsAsync(Guid unitId);
    }
}