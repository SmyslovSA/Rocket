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
            try
            {
                HostFactory.Run(configurator =>
                {
                    configurator.Service<Service>(serviceConfigurator =>
                    {
                        serviceConfigurator.ConstructUsing(name => new Service());
                        serviceConfigurator.WhenStarted((service, control) => service.Start(control));
                        serviceConfigurator.WhenStopped((service, control) => service.Start(control));

                        //Запуск парсера для Lostfilm
                        LostfilmParseProcess(serviceConfigurator);

                        //Запуск парсера для AlbumInfo  
                        AlbumInfoParseProcess(serviceConfigurator);
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
        /// Парсит Lostfilm
        /// </summary>
        /// <param name="serviceConfigurator"></param>
        private static void LostfilmParseProcess(ServiceConfigurator<Service> serviceConfigurator)
        {
            //todo эти настройки должны лежать в базе и задаваться через админку на UI, а пока в конфиге
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["LostfilmParseIsSwitchOn"], out int lostfilmParseIsSwitchOn);
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["LostfilmParsePeriodInMinutes"], out int lostfilmParsePeriodInMinutes);
                        
            if (lostfilmParseIsSwitchOn == 1)
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
        public static void AlbumInfoParseProcess(ServiceConfigurator<Service> serviceConfigurator)
        {
            //todo эти настройки должны лежать в базе и задаваться через админку на UI, а пока в конфиге
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["AlbumInfoParseIsSwitchOn"], out int albumInfoParseIsSwitchOn);
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["AlbumInfoPeriodInMinutes"], out int albumInfoParsingPeriodInMinutes);

            if (albumInfoParseIsSwitchOn == 1)
            {
                Func<ITrigger> albumInfoParseTrigger = () => TriggerBuilder.Create()
                    .WithSimpleSchedule(builder => builder
                        .WithIntervalInMinutes(albumInfoParsingPeriodInMinutes)
                        .WithMisfireHandlingInstructionIgnoreMisfires()
                        .RepeatForever())
                    .Build();
                try
                {
                    IJobDetail albumInfoParseTriggerJob = JobBuilder.Create<ParseAlbumInfoJob>().Build();

                    serviceConfigurator.ScheduleQuartzJob(jobConfigurator =>
                        jobConfigurator
                            .WithJob(() => albumInfoParseTriggerJob)
                            .AddTrigger(albumInfoParseTrigger));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                // Запускает парсер album-info.ru
            }
        }
    }
}
