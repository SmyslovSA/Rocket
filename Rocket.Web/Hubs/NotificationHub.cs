using Microsoft.AspNet.SignalR;

namespace Rocket.Web.Hubs
{
    public class NotificationHub : Hub
    {
        public void SendToAll(string name, string message)
        {
            Clients.All.InvokeAsync("sendToAll", name, message);
        }
    }
}