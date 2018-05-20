using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Quartz;
using Rocket.Notifications.Heplers;
using Rocket.Notifications.Notifications;

namespace Rocket.Notifications.Jobs
{
    /// <inheritdoc />
    /// <summary>
    /// Джоба для отправки push-уведомелений
    /// </summary>
    [DisallowConcurrentExecution]
    internal class PushNotificationsJob : IJob
    {
        /// <inheritdoc />
        /// <summary>
        /// Запуск отправки push-уведомелений 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            //todo логирование 
            try
            {
                var schedulerContext = context.JobDetail.JobDataMap;
                var kernel = (IKernel)schedulerContext.Get(CommonHelper.ContainerKey);

                var pushNotifications = kernel.Get<PushNotifications>();
                pushNotifications.NotifyAsync();
            }
            catch (Exception excpt)
            {
                //todo логирование
                throw excpt;
            }

            //todo логирование отработал
        }

    }
}
