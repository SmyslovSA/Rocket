using System;
using System.Reflection;
using Ninject;
using Quartz;
using Rocket.Parser.Heplers;
using Rocket.Parser.Jobs;
using Topshelf;
using Topshelf.Quartz;
using Topshelf.ServiceConfigurators;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Rocket.Parser
{
    public class Program
    {
        private static void Main()
        {
            try
            {
                //Подключаем Ioc
                var kernel = new StandardKernel();
                kernel.Load(Assembly.GetExecutingAssembly());

                HostFactory.Run(configurator =>
                {
                    configurator.Service<Service>(serviceConfigurator =>
                    {
                        serviceConfigurator.ConstructUsing(name => new Service());
                        serviceConfigurator.WhenStarted((service, control) => service.Start(control));
                        serviceConfigurator.WhenStopped((service, control) => service.Start(control));

                        //Запуск парсера для Lostfilm
                        LostfilmParseProcess(serviceConfigurator, kernel);

                        //Запуск парсера для AlbumInfo  
                        AlbumInfoParseProcess(serviceConfigurator, kernel);
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
        /// Добавляет в конфигуратор Quartz задание по расписанию для парсинга Lostfilm.
        /// </summary>
        /// <param name="serviceConfigurator">Конфигуратор Quartz.</param>
        /// <param name="kernel">DI контейнер</param>
        private static void LostfilmParseProcess(ServiceConfigurator<Service> serviceConfigurator, IKernel kernel)
        {
            //todo эти настройки должны лежать в базе и задаваться через админку на UI, а пока в конфиге
            bool.TryParse(ConfigurationManager.AppSettings[SettingsHelper.LostfilmParseIsSwitchOnKey],
                out bool lostfilmParseIsSwitchOn);
            int.TryParse(ConfigurationManager.AppSettings[SettingsHelper.LostfilmParsePeriodInMinutesKey],
                out int lostfilmParsePeriodInMinutes);

            if (lostfilmParseIsSwitchOn)
            {
                Func<ITrigger> lostfilmParseTrigger = () => TriggerBuilder.Create()
                    .WithSimpleSchedule(builder => builder
                        .WithIntervalInMinutes(lostfilmParsePeriodInMinutes)
                        .WithMisfireHandlingInstructionIgnoreMisfires()
                        .RepeatForever())
                    .Build();

                // Запускает парсер Lostfilm
                var lostfilmParseTriggerJob = JobBuilder.Create<LostfilmParseJob>().Build();
                lostfilmParseTriggerJob.JobDataMap.Put("container", kernel);

                serviceConfigurator.ScheduleQuartzJob(jobConfigurator =>
                    jobConfigurator
                        .WithJob(() => lostfilmParseTriggerJob)
                        .AddTrigger(lostfilmParseTrigger));
            }
        }

        /// <summary>
        /// Добавляет в конфигуратор Quartz задание по расписанию для парсинга album-info.ru.
        /// </summary>
        /// <param name="serviceConfigurator">Конфигуратор Quartz.</param>
        /// <param name="kernel">DI контейнер</param>
        public static void AlbumInfoParseProcess(ServiceConfigurator<Service> serviceConfigurator, IKernel kernel)
        {
            //todo эти настройки должны лежать в базе и задаваться через админку на UI, а пока в конфиге
            bool.TryParse(ConfigurationManager.AppSettings[SettingsHelper.AlbumInfoParseIsSwitchOnKey],
                out bool albumInfoParseIsSwitchOn);
            int.TryParse(ConfigurationManager.AppSettings[SettingsHelper.AlbumInfoPeriodInMinutesKey],
                out int albumInfoParsingPeriodInMinutes);

            if (albumInfoParseIsSwitchOn)
            {
                Func<ITrigger> albumInfoParseTrigger = () => TriggerBuilder.Create()
                    .WithSimpleSchedule(builder => builder
                        .WithIntervalInMinutes(albumInfoParsingPeriodInMinutes)
                        .WithMisfireHandlingInstructionIgnoreMisfires()
                        .RepeatForever())
                    .Build();

                // Запускает парсер album-info.ru
                IJobDetail albumInfoParseTriggerJob = JobBuilder.Create<AlbumInfoParseJob>().Build();
                albumInfoParseTriggerJob.JobDataMap.Put("container", kernel);

                serviceConfigurator.ScheduleQuartzJob(jobConfigurator =>
                    jobConfigurator
                        .WithJob(() => albumInfoParseTriggerJob)
                        .AddTrigger(albumInfoParseTrigger));
            }
        }
    }
}
