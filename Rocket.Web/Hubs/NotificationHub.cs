using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Rocket.Web.Hubs
{
    [HubName("notification")]
    public class NotificationHub : Hub
    {
        private static readonly IHubContext HubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();

        [HubMethodName("notifyAll")]
        public static void NotifyAll(object msg)
        {
            HubContext.Clients.All.notifyAll(msg);
        }
    }
}