using BookingService.Domain.Entities;

namespace BookingService.Domain.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<IReadOnlyList<Booking>> GetByResidentIdAsync(Guid residentId);
        Task<IReadOnlyList<Booking>> GetByAmenityIdAndDateRangeAsync(Guid amenityId, DateTime startDate, DateTime endDate);
    }
}