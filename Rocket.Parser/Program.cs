using System;
using Quartz;
using Rocket.Parser.Jobs;
using Topshelf;
using Topshelf.Quartz;
using Topshelf.ServiceConfigurators;

namespace Rocket.Parser
{
    public class Program
    {

        static void Main(string[] args)
        {
            HostFactory.Run(configurator =>
            {
                try
                {
                    configurator.Service<Service>(serviceConfigurator =>
                    {
                        serviceConfigurator.ConstructUsing(name => new Service());
                        serviceConfigurator.WhenStarted((service, control) => service.Start(control));
                        serviceConfigurator.WhenStopped((service, control) => service.Start(control));

                        // Запуск парсера для Lostfilm
                        LostfilmParse(serviceConfigurator);

                        ParseAlbumInfoJob(serviceConfigurator);

                    });

                    configurator.StartAutomatically();
                }
                catch (Exception e)
                {
                    // todo log
                    throw;
                }
            });
        }

        /// <summary>
        /// Парсит Lostfilm
        /// </summary>
        /// <param name="serviceConfigurator"></param>
        private static void LostfilmParse(ServiceConfigurator<Service> serviceConfigurator)
        {
            string errorMessage = "";

            int isSwitchOnLostfilmParse = 1;
            int lostfilmParsePeriodInMinutes = 150;




            if (isSwitchOnLostfilmParse == 1)
            {
                Func<ITrigger> lostfilmParseTrigger = () => TriggerBuilder.Create()
                    .WithSimpleSchedule(builder => builder
                        .WithIntervalInMinutes(lostfilmParsePeriodInMinutes)
                        .WithMisfireHandlingInstructionIgnoreMisfires()
                        .RepeatForever())
                    .Build();

                // Запускает парсер Lostfilm
                IJobDetail lostfilmParseTriggerJob = JobBuilder.Create<LostfilmParseJob>().Build();

                serviceConfigurator.ScheduleQuartzJob(jobConfigurator =>
                    jobConfigurator
                        .WithJob(() => lostfilmParseTriggerJob)
                        .AddTrigger(lostfilmParseTrigger));
            }
        }

        /// <summary>
        /// Парсит сайт album-info.ru
        /// </summary>
        /// <param name="serviceConfigurator"></param>
        public static void ParseAlbumInfoJob(ServiceConfigurator<Service> serviceConfigurator)
        {
            string errorMessage = "";

            int isSwitchOnLostfilmParse = 1;
            int albumInfoParsingPeriodInMinutes = 150;




            if (isSwitchOnLostfilmParse == 1)
            {
                Func<ITrigger> albumInfoParseTrigger = () => TriggerBuilder.Create()
                    .WithSimpleSchedule(builder => builder
                        .WithIntervalInMinutes(albumInfoParsingPeriodInMinutes)
                        .WithMisfireHandlingInstructionIgnoreMisfires()
                        .RepeatForever())
                    .Build();

                // Запускает парсер album-info.ru
                IJobDetail albumInfoParseTriggerJob = JobBuilder.Create<ParseAlbumInfoJob>().Build();

                serviceConfigurator.ScheduleQuartzJob(jobConfigurator =>
                    jobConfigurator
                        .WithJob(() => albumInfoParseTriggerJob)
                        .AddTrigger(albumInfoParseTrigger));
            }
        }
    }
}
