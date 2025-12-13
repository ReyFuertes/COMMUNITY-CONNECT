using BookingService.Domain.Entities;

namespace BookingService.Domain.Interfaces
{
    public interface IAmenityRepository : IRepository<Amenity>
    {
        Task<Amenity?> GetByNameAsync(string name);
    }
}