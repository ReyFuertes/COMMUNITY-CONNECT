namespace UserAndUnitManagement.Domain.Entities
{
    public class Unit
    {
        public Guid Id { get; set; }
        public required string Name { get; set; } // e.g., "Tower A, Unit 1201"
        public UnitStatus Status { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string ZipCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        // Navigation property for relationships
        public ICollection<UserUnit>? UserUnits { get; set; } // Many-to-many with User
    }

    public enum UnitStatus
    {
        OwnerOccupied,
        Tenanted,
        Vacant
    }
}