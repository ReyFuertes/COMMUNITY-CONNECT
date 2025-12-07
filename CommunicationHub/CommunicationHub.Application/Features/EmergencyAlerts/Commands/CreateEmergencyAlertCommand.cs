using CommunicationHub.Application.Dtos;
using CommunicationHub.Domain.Enums;
using MediatR;

namespace CommunicationHub.Application.Features.EmergencyAlerts.Commands
{
    public class CreateEmergencyAlertCommand : IRequest<EmergencyAlertDto>
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public EmergencyAlertType Type { get; set; }
        public Guid AuthorId { get; set; }
    }
}
