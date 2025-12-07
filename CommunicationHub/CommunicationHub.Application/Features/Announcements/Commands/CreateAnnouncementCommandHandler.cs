using CommunicationHub.Application.Dtos;
using CommunicationHub.Application.Interfaces;
using CommunicationHub.Domain.Entities;
using CommunicationHub.Domain.Interfaces;
using MediatR;

namespace CommunicationHub.Application.Features.Announcements.Commands
{
    public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, AnnouncementDto>
    {
        private readonly IRepository<Announcement> _repository;
        private readonly INotificationService _notificationService;

        public CreateAnnouncementCommandHandler(IRepository<Announcement> repository, INotificationService notificationService)
        {
            _repository = repository;
            _notificationService = notificationService;
        }

        public async Task<AnnouncementDto> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var announcement = new Announcement
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content,
                Category = request.Category,
                CreatedDate = DateTime.UtcNow,
                ScheduledDate = request.ScheduledDate,
                ExpirationDate = request.ExpirationDate,
                AuthorId = request.AuthorId
            };

            await _repository.AddAsync(announcement);

            var dto = new AnnouncementDto
            {
                Id = announcement.Id,
                Title = announcement.Title,
                Content = announcement.Content,
                Category = announcement.Category.ToString(),
                CreatedDate = announcement.CreatedDate
            };

            // Broadcast via SignalR
            await _notificationService.BroadcastAnnouncementAsync(dto);

            return dto;
        }
    }
}
