using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace WebClient.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotificationToClient(string message, int consultantId)
        {
                await Clients.Client(Context.ConnectionId).SendAsync("ReceivedPersonalNotification", message, consultantId);
        }
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }

    }
}
