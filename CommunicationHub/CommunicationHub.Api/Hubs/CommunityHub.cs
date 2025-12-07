using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CommunicationHub.Api.Hubs
{
    [Authorize]
    public class CommunityHub : Hub
    {
        // Clients can join specific conversation groups to receive real-time updates for that chat
        public async Task JoinConversationGroup(string conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
        }

        public async Task LeaveConversationGroup(string conversationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId);
        }
        
        // OnConnectedAsync can be used to map the user's ID (from claims) to their connection if we wanted to send direct messages by UserId.
        // For now, we rely on Groups for conversations and "All" for broadcasts.
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
    }
}
