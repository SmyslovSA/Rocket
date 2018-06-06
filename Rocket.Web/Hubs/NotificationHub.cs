﻿using Microsoft.AspNet.SignalR;
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

        [HubMethodName("notifyOfRelease")]
        public static void NotifyOfRelease(object msg, string[] users)
        {
            HubContext.Clients.Users(users).notifyOfRelease(msg);
        }
    }
}