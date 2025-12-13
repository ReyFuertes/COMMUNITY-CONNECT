using Microsoft.EntityFrameworkCore;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces;
using BookingService.Infrastructure.Persistence;

namespace BookingService.Infrastructure.Repositories
{
    public class AmenityRepository : GenericRepository<Amenity>, IAmenityRepository
    {
        public AmenityRepository(BookingDbContext context) : base(context) { }

        public async Task<Amenity?> GetByNameAsync(string name)
        {
            return await _context.Amenities
                .Include(a => a.Rules)
                .FirstOrDefaultAsync(a => a.Name == name);
        }

        public override async Task<Amenity?> GetByIdAsync(Guid id)
        {
            return await _context.Amenities
                .Include(a => a.Rules)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}