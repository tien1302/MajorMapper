using BAL.Services.Interfaces;
using BAL.DTOs.Notifications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebClient.Interface;

namespace WebClient.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IUserConnectionManager _userConnectionManager;
        public INotificationService _Service;

        public NotificationHub(IUserConnectionManager userConnectionManager, INotificationService Service)
        {
            _userConnectionManager = userConnectionManager;
            _Service = Service;
        }
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }
        public string GetConnectionId(string userId)
        {
            _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);

            return Context.ConnectionId;
        }

        //Called when a connection with the hub is terminated.
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            //get the connectionId
            var connectionId = Context.ConnectionId;
            _userConnectionManager.RemoveUserConnection(connectionId);
            var value = await Task.FromResult(0);//adding dump code to follow the template of Hub > OnDisconnectedAsync
        }



        public async Task SendNotificationToClient(string tilte, string content, string consultantId)
        {
            var connections = _userConnectionManager.GetUserConnections(consultantId);
            if(connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {
                    await Clients.Client(connectionId).SendAsync("ReceivedPersonalNotification", tilte, content);
                }
            }
        }

        public async Task SendNotifications(string consultantId)
        {
            List<GetNotification> notifications = _Service.GetAllByConsultantId(consultantId);
            await Clients.All.SendAsync("ReceivedNotifications", notifications);

        }




    }
}
