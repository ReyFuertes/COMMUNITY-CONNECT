using Microsoft.EntityFrameworkCore;
using CommunicationHub.Domain.Entities;

namespace CommunicationHub.Infrastructure.Persistence
{
    public class CommunicationHubDbContext : DbContext
    {
        public CommunicationHubDbContext(DbContextOptions<CommunicationHubDbContext> options) : base(options)
        {
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementAttachment> AnnouncementAttachments { get; set; }
        public DbSet<EmergencyAlert> EmergencyAlerts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationParticipant> ConversationParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Announcement Attachments
            modelBuilder.Entity<Announcement>()
                .HasMany(a => a.Attachments)
                .WithOne(at => at.Announcement)
                .HasForeignKey(at => at.AnnouncementId)
                .OnDelete(DeleteBehavior.Cascade);

            // Conversation Participants
            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Participants)
                .WithOne(p => p.Conversation)
                .HasForeignKey(p => p.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Conversation Messages
            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Conversation)
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data for Contacts (Directory)
            modelBuilder.Entity<Contact>().HasData(
                new Contact { Id = Guid.NewGuid(), Name = "Admin Office", PhoneNumber = "555-0100", Role = "Admin", Description = "Main office for general inquiries", IsEmergencyNumber = false },
                new Contact { Id = Guid.NewGuid(), Name = "Security Desk", PhoneNumber = "555-0101", Role = "Security", Description = "24/7 Security Desk", IsEmergencyNumber = true },
                new Contact { Id = Guid.NewGuid(), Name = "Emergency Services", PhoneNumber = "911", Role = "Emergency", Description = "Public Emergency Services", IsEmergencyNumber = true }
            );
        }
    }
}
