using CommunicationHub.Application.Dtos;
using CommunicationHub.Application.Interfaces;
using CommunicationHub.Domain.Entities;
using CommunicationHub.Domain.Interfaces;
using MediatR;

namespace CommunicationHub.Application.Features.EmergencyAlerts.Commands
{
    public class CreateEmergencyAlertCommandHandler : IRequestHandler<CreateEmergencyAlertCommand, EmergencyAlertDto>
    {
        private readonly IRepository<EmergencyAlert> _repository;
        private readonly INotificationService _notificationService;

        public CreateEmergencyAlertCommandHandler(IRepository<EmergencyAlert> repository, INotificationService notificationService)
        {
            _repository = repository;
            _notificationService = notificationService;
        }

        public async Task<EmergencyAlertDto> Handle(CreateEmergencyAlertCommand request, CancellationToken cancellationToken)
        {
            var alert = new EmergencyAlert
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Type = request.Type,
                SentDate = DateTime.UtcNow,
                AuthorId = request.AuthorId,
                IsActive = true
            };

            await _repository.AddAsync(alert);

            var dto = new EmergencyAlertDto
            {
                Id = alert.Id,
                Title = alert.Title,
                Description = alert.Description,
                Type = alert.Type.ToString(),
                SentDate = alert.SentDate
            };

            // Broadcast immediate alert
            await _notificationService.BroadcastEmergencyAlertAsync(dto);

            return dto;
        }
    }
}
