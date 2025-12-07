using Microsoft.EntityFrameworkCore;
using MaintenanceService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Design;

namespace MaintenanceService.Infrastructure.Persistence;

public class MaintenanceDbContext : DbContext
{
    public MaintenanceDbContext(DbContextOptions<MaintenanceDbContext> options) : base(options) { }

    public DbSet<WorkOrder> WorkOrders { get; set; }
    public DbSet<WorkOrderAttachment> WorkOrderAttachments { get; set; }
    public DbSet<WorkOrderNote> WorkOrderNotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WorkOrder>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.Category).IsRequired();
            entity.Property(e => e.UrgencyLevel).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.UnitId).IsRequired();
            entity.Property(e => e.RequesterId).IsRequired();
            // AssignedToId can be null

            entity.HasMany(e => e.Attachments)
                  .WithOne(a => a.WorkOrder)
                  .HasForeignKey(a => a.WorkOrderId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Notes)
                  .WithOne(n => n.WorkOrder)
                  .HasForeignKey(n => n.WorkOrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<WorkOrderAttachment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.FilePath).IsRequired().HasMaxLength(500);
            entity.Property(e => e.FileName).IsRequired().HasMaxLength(250);
        });

        modelBuilder.Entity<WorkOrderNote>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Content).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.CreatedBy).IsRequired();
        });

        // Configure BaseEntity properties
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType).Property<DateTime>("CreatedDate")
                    .HasDefaultValueSql("GETDATE()");
                modelBuilder.Entity(entityType.ClrType).Property<DateTime?>("LastModifiedDate")
                    .IsRequired(false);
            }
        }
    }

    public override int SaveChanges()
    {
        UpdateBaseEntityDates();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateBaseEntityDates();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateBaseEntityDates()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).LastModifiedDate = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
            }
        }
    }
}

// Factory for design-time migrations
public class MaintenanceDbContextFactory : IDesignTimeDbContextFactory<MaintenanceDbContext>
{
    public MaintenanceDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MaintenanceDbContext>();
        // This connection string is for design-time tooling. Replace with your actual connection string for development.
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MaintenanceDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new MaintenanceDbContext(optionsBuilder.Options);
    }
}
