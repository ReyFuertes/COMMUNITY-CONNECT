using Microsoft.EntityFrameworkCore;
using BookingService.Domain.Entities;

namespace BookingService.Infrastructure.Persistence
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingRule> BookingRules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Amenity>()
                .HasMany(a => a.Bookings)
                .WithOne(b => b.Amenity)
                .HasForeignKey(b => b.AmenityId);
            
            modelBuilder.Entity<Amenity>()
                .HasMany(a => a.Rules)
                .WithOne(r => r.Amenity)
                .HasForeignKey(r => r.AmenityId);

            // Configure decimal precision
            modelBuilder.Entity<Amenity>()
                .Property(a => a.BookingFee)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Amenity>()
                .Property(a => a.SecurityDeposit)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Booking>()
                .Property(b => b.PaymentTransactionId)
                .IsRequired(false); // Can be null if no payment is required
        }
    }
}