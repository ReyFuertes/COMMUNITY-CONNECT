namespace UserAndUnitManagement.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; } // Enum for roles
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool OptInToDirectory { get; set; }
        public bool ShowEmailInDirectory { get; set; }

        // Navigation properties for relationships
        public ICollection<UserUnit> UserUnits { get; set; } // Many-to-many with Unit
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Pet> Pets { get; set; }
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