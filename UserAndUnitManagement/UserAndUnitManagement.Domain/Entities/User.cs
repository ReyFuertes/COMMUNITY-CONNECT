namespace UserAndUnitManagement.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Salt { get; set; }
        public UserRole Role { get; set; } // Enum for roles
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool OptInToDirectory { get; set; }
        public bool ShowEmailInDirectory { get; set; }

        // Navigation properties for relationships
        public ICollection<UserUnit>? UserUnits { get; set; } // Many-to-many with Unit
        public ICollection<Vehicle>? Vehicles { get; set; }
        public ICollection<Pet>? Pets { get; set; }
    }

    public enum UserRole
    {
        SuperAdmin,
        PropertyManager,
        SecurityGuard,
        Owner,
        Tenant
    }
}