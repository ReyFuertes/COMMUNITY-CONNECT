using CommunicationHub.Api.Hubs;
using CommunicationHub.Application.Dtos;
using CommunicationHub.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace CommunicationHub.Api.Services
{
    public class SignalRNotificationService : INotificationService
    {
        private readonly IHubContext<CommunityHub> _hubContext;

        public SignalRNotificationService(IHubContext<CommunityHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task BroadcastAnnouncementAsync(AnnouncementDto announcement)
        {
            // "ReceiveAnnouncement" is the event name clients listen for
            await _hubContext.Clients.All.SendAsync("ReceiveAnnouncement", announcement);
        }

        public async Task BroadcastEmergencyAlertAsync(EmergencyAlertDto alert)
        {
             // "ReceiveEmergencyAlert" is the event name clients listen for
            await _hubContext.Clients.All.SendAsync("ReceiveEmergencyAlert", alert);
        }

        public async Task SendMessageNotificationAsync(Guid conversationId, MessageDto message)
        {
             // "ReceiveMessage" is the event name clients listen for
            await _hubContext.Clients.Group(conversationId.ToString()).SendAsync("ReceiveMessage", message);
        }
    }
}
