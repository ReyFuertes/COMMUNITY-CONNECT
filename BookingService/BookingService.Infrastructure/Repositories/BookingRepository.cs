using Microsoft.EntityFrameworkCore;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces;
using BookingService.Infrastructure.Persistence;

namespace BookingService.Infrastructure.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingDbContext context) : base(context) { }

        public override async Task<Booking?> GetByIdAsync(Guid id)
        {
            return await _context.Bookings
                .Include(b => b.Amenity)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IReadOnlyList<Booking>> GetByResidentIdAsync(Guid residentId)
        {
            return await _context.Bookings
                .Include(b => b.Amenity)
                .Where(b => b.ResidentId == residentId)
                .OrderByDescending(b => b.StartTime)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Booking>> GetByAmenityIdAndDateRangeAsync(Guid amenityId, DateTime startDate, DateTime endDate)
        {
            return await _context.Bookings
                .Where(b => b.AmenityId == amenityId && b.StartTime >= startDate && b.EndTime <= endDate)
                .ToListAsync();
        }
    }
}