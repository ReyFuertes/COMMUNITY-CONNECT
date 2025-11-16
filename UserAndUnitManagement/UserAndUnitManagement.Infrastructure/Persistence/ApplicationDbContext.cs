using Microsoft.EntityFrameworkCore;
using UserAndUnitManagement.Domain.Entities;

namespace UserAndUnitManagement.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UserUnit> UserUnits { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between User and Unit
            modelBuilder.Entity<UserUnit>()
                .HasKey(uu => new { uu.UserId, uu.UnitId });

            modelBuilder.Entity<UserUnit>()
                .HasOne(uu => uu.User)
                .WithMany(u => u.UserUnits)
                .HasForeignKey(uu => uu.UserId);

            modelBuilder.Entity<UserUnit>()
                .HasOne(uu => uu.Unit)
                .WithMany(u => u.UserUnits)
                .HasForeignKey(uu => uu.UnitId);

            // Configure one-to-many relationship between User and Vehicle
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.User)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.UserId);

            // Configure one-to-many relationship between User and Pet
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.User)
                .WithMany(u => u.Pets)
                .HasForeignKey(p => p.UserId);

            // Seed Data
            var superAdminId = new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef");
            var propertyManagerId = new Guid("b2c3d4e5-f6a1-0987-5432-10fedcba9876");
            var securityGuardId = new Guid("c3d4e5f6-a1b2-7890-1234-567890abcdef");
            var ownerId = new Guid("d4e5f6a1-b2c3-0987-5432-10fedcba9876");
            var tenantId = new Guid("e5f6a1b2-c3d4-7890-1234-567890abcdef");

            modelBuilder.Entity<User>().HasData(
                new User { Id = superAdminId, FirstName = "Super", LastName = "Admin", Email = "superadmin@example.com", PasswordHash = "7f2d81eb026f9debd54b30640d8a3c1919e2668294fbb0cbb5e4eccb4c761113", Salt = "af72b314-4a9d-4ea3-9bd6-5d5a55e0ce0b", Role = UserRole.SuperAdmin, CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsActive = true, OptInToDirectory = true, ShowEmailInDirectory = true },
                new User { Id = propertyManagerId, FirstName = "Property", LastName = "Manager", Email = "propertymanager@example.com", PasswordHash = "4f62a04a1f7c705d4471ac2a93a2c3affcfe5b4846d67e97d723ae2ed898e69b", Salt = "7dda33b8-34f3-4f79-b6c2-73f726b91cb5", Role = UserRole.PropertyManager, CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsActive = true, OptInToDirectory = true, ShowEmailInDirectory = true },
                new User { Id = securityGuardId, FirstName = "Security", LastName = "Guard", Email = "securityguard@example.com", PasswordHash = "e35ec334b374a96bfede2e97d534429ad44d0eac691d36ff2c1004313e5f2ce3", Salt = "1c64f8b2-fd5c-47d1-bcd5-969b4fe7f819", Role = UserRole.SecurityGuard, CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsActive = true, OptInToDirectory = true, ShowEmailInDirectory = true },
                new User { Id = ownerId, FirstName = "Owner", LastName = "User", Email = "owner@example.com", PasswordHash = "f521e197e6b9c1eea7f68c63c7d334e04e124a4443f162adbe0821e8ec86df0f", Salt = "11d07415-1328-4a81-9605-a70fe8332a22", Role = UserRole.Owner, CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsActive = true, OptInToDirectory = true, ShowEmailInDirectory = true },
                new User { Id = tenantId, FirstName = "Tenant", LastName = "User", Email = "tenant@example.com", PasswordHash = "1b45afdfbddbc1196bf03c98af409829a168c5e376e418832e1ac495702d2307", Salt = "cc4f60ed-23d8-45b6-89bd-6ac9be3c828d", Role = UserRole.Tenant, CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsActive = true, OptInToDirectory = true, ShowEmailInDirectory = true }
            );

            modelBuilder.Entity<Unit>().HasData(
                new Unit { Id = new Guid("a1b2c3d4-e5f6-7890-1234-567890abcde0"), Name = "Unit 101", Address = "123 Main St", City = "Anytown", State = "CA", ZipCode = "12345" },
                new Unit { Id = new Guid("fedcba09-8765-4321-0987-654321fedcb0"), Name = "Unit 202", Address = "456 Oak Ave", City = "Someville", State = "NY", ZipCode = "54321" }
            );

            modelBuilder.Entity<Pet>().HasData(
                new Pet { Id = new Guid("a1b2c3d4-e5f6-7890-1234-567890abcde1"), Name = "Buddy", Species = "Dog", Breed = "Golden Retriever", UserId = ownerId, PhotoUrl = "" },
                new Pet { Id = new Guid("fedcba09-8765-4321-0987-654321fedcb1"), Name = "Whiskers", Species = "Cat", Breed = "Siamese", UserId = ownerId, PhotoUrl = "" }
            );

            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { Id = new Guid("a1b2c3d4-e5f6-7890-1234-567890abcde2"), Make = "Toyota", Model = "Camry", PlateNumber = "123-ABC", Color = "Silver", UserId = tenantId },
                new Vehicle { Id = new Guid("fedcba09-8765-4321-0987-654321fedcb2"), Make = "Honda", Model = "Civic", PlateNumber = "456-DEF", Color = "Black", UserId = tenantId }
            );
        }
    }
}