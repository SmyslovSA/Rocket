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

                        // Создает заказы и возвраты в наве по заказам портала которые нужно передать в нав.
                        LostfilmParse(serviceConfigurator);

                    });

                    configurator.StartAutomatically();
                }
                catch (Exception e)
                {
                    throw;
                }
            });

            //    var lostfilmParseService = new LostfilmParseService();

            //    var test = Task.Run(() => lostfilmParseService.Parse()).Result;

            //    Console.WriteLine("Use commands:");
            //    Console.WriteLine("/start -- start parsing");
            //    Console.WriteLine("/abort -- abort parsing");
            //    Console.WriteLine("/quit -- close the app");

            //    var quitNow = false;
            //    while (!quitNow)
            //    {
            //        var parser = new Middleware();

            //        var command = Console.ReadLine();
            //        switch (command)
            //        {
            //            case "/start":
            //                Console.Write("Write start page:");
            //                var startPage = Convert.ToInt32(Console.ReadLine());

            //                Console.Write("Write end page:");
            //                var endPage = Convert.ToInt32(Console.ReadLine());

            //                parser.Start(startPage, endPage);

            //                break;

            //            case "/abort":
            //                parser.Abort();
            //                Console.WriteLine("App was abort");
            //                break;

            //            case "/quit":
            //                parser.Abort();
            //                quitNow = true;
            //                break;

            //            default:
            //                Console.WriteLine("Unknown Command " + command);
            //                break;
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Создает заказы и возвраты в наве по заказам портала которые нужно передать в нав.
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

                // Запускает создание заказов в наве по заказам портала которые нужно передать в нав.
                IJobDetail lostfilmParseTriggerJob = JobBuilder.Create<LostfilmParseJob>().Build();

                serviceConfigurator.ScheduleQuartzJob(jobConfigurator =>
                    jobConfigurator
                        .WithJob(() => lostfilmParseTriggerJob)
                        .AddTrigger(lostfilmParseTrigger));
            }
        }
    }
}
