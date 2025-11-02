namespace UserAndUnitManagement.Domain.Entities
{
    public class Unit
    {
        public Guid Id { get; set; }
        public string Name { get; set; } // e.g., "Tower A, Unit 1201"
        public UnitStatus Status { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        // Navigation property for relationships
        public ICollection<UserUnit> UserUnits { get; set; } // Many-to-many with User
    }

    public enum UnitStatus
    {
        OwnerOccupied,
        Tenanted,
        Vacant
    }
}