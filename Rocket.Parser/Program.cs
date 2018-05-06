using System;
using Ninject;
using Quartz;
using Rocket.Parser.Heplers;
using Rocket.Parser.Jobs;
using Topshelf;
using Topshelf.Quartz;
using Topshelf.ServiceConfigurators;

namespace Rocket.Parser
{
    public class Program
    {
        private static void Main()
        {
            try
            {
                //Подключаем Ioc
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var kernel = BootstrapHelper.LoadNinjectKernel(assemblies);

                HostFactory.Run(configurator =>
                {
                    //конфигурируем Topshelf.Quartz
                    configurator.Service<Service>(serviceConfigurator =>
                    {
                        serviceConfigurator.ConstructUsing(name => new Service());
                        serviceConfigurator.WhenStarted((service, control) => service.Start(control));
                        serviceConfigurator.WhenStopped((service, control) => service.Stop(control));

                        //Запуск парсера для Lostfilm
                        //LostfilmParseProcess(serviceConfigurator, kernel);

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
            bool.TryParse(LostfilmHelper.GetParseIsSwitchOn(), out bool lostfilmParseIsSwitchOn);
            int.TryParse(LostfilmHelper.GetParsePeriodInMinutes(), out int lostfilmParsePeriodInMinutes);

            if (!lostfilmParseIsSwitchOn) return;

            ITrigger LostfilmParseTrigger() => TriggerBuilder.Create()
                .WithSimpleSchedule(builder => builder.WithIntervalInMinutes(lostfilmParsePeriodInMinutes)
                    .WithMisfireHandlingInstructionIgnoreMisfires()
                    .RepeatForever())
                .Build();

            // Запускает парсер Lostfilm
            var lostfilmParseTriggerJob = JobBuilder.Create<LostfilmParseJob>().Build();
            lostfilmParseTriggerJob.JobDataMap.Put(CommonHelper.ContainerKey, kernel);

            serviceConfigurator.ScheduleQuartzJob(jobConfigurator =>
                jobConfigurator
                    .WithJob(() => lostfilmParseTriggerJob)
                    .AddTrigger(LostfilmParseTrigger));
        }

        /// <summary>
        /// Добавляет в конфигуратор Quartz задание по расписанию для парсинга album-info.ru.
        /// </summary>
        /// <param name="serviceConfigurator">Конфигуратор Quartz.</param>
        /// <param name="kernel">DI контейнер</param>
        public static void AlbumInfoParseProcess(ServiceConfigurator<Service> serviceConfigurator, IKernel kernel)
        {
            //todo эти настройки должны лежать в базе и задаваться через админку на UI, а пока в конфиге
            bool.TryParse(AlbumInfoHelper.GetParseIsSwitchOn(), out bool albumInfoParseIsSwitchOn);
            int.TryParse(AlbumInfoHelper.GetParsePeriodInMinutes(), out int albumInfoParsingPeriodInMinutes);

            if (albumInfoParseIsSwitchOn)
            {
                ITrigger AlbumInfoParseTrigger() => TriggerBuilder.Create()
                    .WithSimpleSchedule(builder => builder.WithIntervalInMinutes(albumInfoParsingPeriodInMinutes)
                        .WithMisfireHandlingInstructionIgnoreMisfires()
                        .RepeatForever())
                    .Build();

                // Запускает парсер album-info.ru
                IJobDetail albumInfoParseTriggerJob = JobBuilder.Create<AlbumInfoParseJob>().Build();
                albumInfoParseTriggerJob.JobDataMap.Put(CommonHelper.ContainerKey, kernel);

                serviceConfigurator.ScheduleQuartzJob(jobConfigurator =>
                    jobConfigurator
                        .WithJob(() => albumInfoParseTriggerJob)
                        .AddTrigger(AlbumInfoParseTrigger));
            }
        }
    }
}
