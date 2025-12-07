using CommunicationHub.Application.Dtos;

namespace CommunicationHub.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendMessageNotificationAsync(Guid conversationId, MessageDto message);
        Task BroadcastAnnouncementAsync(AnnouncementDto announcement);
        Task BroadcastEmergencyAlertAsync(EmergencyAlertDto alert);
    }
}
