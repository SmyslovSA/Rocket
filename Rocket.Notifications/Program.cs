using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Quartz;
using Rocket.DAL.Common.DbModels.Notification;
using Rocket.DAL.Common.Repositories;
using Rocket.Notifications.Heplers;
using Rocket.Notifications.Jobs;
using Rocket.Notifications.Properties;
using Rocket.Notifications.Services;
using Topshelf;
using Topshelf.Quartz;
using Topshelf.ServiceConfigurators;

namespace Rocket.Notifications
{
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                //Подключаем IoC
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var kernel = BootstrapHelper.LoadNinjectKernel(assemblies);

                HostFactory.Run(configurator =>
                {
                    //конфигурируем Topshelf.Quartz
                    configurator.Service<TopshelfService>(serviceConfigurator =>
                    {
                        serviceConfigurator.ConstructUsing(name => new TopshelfService());
                        serviceConfigurator.WhenStarted((service, control) => service.Start(control));
                        serviceConfigurator.WhenStopped((service, control) => service.Stop(control));

                        //Запуск процесса рассылки push-уведомлений 
                        PushNotificationsProcess(serviceConfigurator, kernel);
                    });

                    configurator.StartAutomatically();
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Добавляет в конфигуратор Quartz задание по расписанию для push-уведомлений.
        /// </summary>
        /// <param name="serviceConfigurator">Конфигуратор Quartz.</param>
        /// <param name="kernel">DI контейнер</param>
        private static void PushNotificationsProcess(
            ServiceConfigurator<TopshelfService> serviceConfigurator, IKernel kernel)
        {
            var notificationsSettingsRepository = kernel.Get<IBaseRepository<NotificationsSettingsEntity>>();
            var resource = notificationsSettingsRepository
                .Queryable().First(r => r.Name.Equals(Resources.PushNotificationsSettings));

            var pushNotificationsIsSwitchOn = resource.NotifyIsSwitchOn;
            var pushNotificationsPeriodInMinutes = resource.NotifyPeriodInMinutes;

            if (!pushNotificationsIsSwitchOn) return;

            ITrigger PushNotificationsTrigger() => TriggerBuilder.Create()
                .WithSimpleSchedule(builder => builder.WithIntervalInMinutes(pushNotificationsPeriodInMinutes)
                    .WithMisfireHandlingInstructionIgnoreMisfires()
                    .RepeatForever())
                .Build();

            IJobDetail pushNotificationsTriggerJob = JobBuilder.Create<PushNotificationsJob>().Build();
            pushNotificationsTriggerJob.JobDataMap.Put(CommonHelper.ContainerKey, kernel);

            // Запускает push-уведомления
            serviceConfigurator.ScheduleQuartzJob(jobConfigurator =>
                jobConfigurator
                    .WithJob(() => pushNotificationsTriggerJob)
                    .AddTrigger(PushNotificationsTrigger));
        }

    }
}
