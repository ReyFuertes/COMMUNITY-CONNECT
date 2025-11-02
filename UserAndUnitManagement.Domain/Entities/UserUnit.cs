namespace UserAndUnitManagement.Domain.Entities
{
    public class UserUnit
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }

        public DateTime AssignedDate { get; set; }
        public DateTime? UnassignedDate { get; set; }
        public bool IsPrimary { get; set; } // e.g., primary owner/tenant
    }
}