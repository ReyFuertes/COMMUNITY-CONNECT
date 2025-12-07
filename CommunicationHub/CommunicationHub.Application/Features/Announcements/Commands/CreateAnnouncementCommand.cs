using CommunicationHub.Application.Dtos;
using CommunicationHub.Domain.Enums;
using MediatR;

namespace CommunicationHub.Application.Features.Announcements.Commands
{
    public class CreateAnnouncementCommand : IRequest<AnnouncementDto>
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public AnnouncementCategory Category { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public Guid AuthorId { get; set; }
    }
}
