using Ninject.Modules;
using Rocket.DAL.Context;
using Rocket.Notifications.Interfaces;
using Rocket.Notifications.Notifications;

namespace Rocket.Notifications.Config
{
    public class NotificationsModule : NinjectModule
    {
        /// <summary>
        /// Настройка Ninject для уведомлений
        /// </summary>
        public override void Load()
        {
            Rebind<RocketContext>().ToMethod(ctx => new RocketContext()).InSingletonScope(); //?
            Bind<IPushNotifications>().To<PushNotifications>();
        }
    }
}
