using System;
using Ninject;
using Quartz;
using Rocket.Parser.Heplers;
using Rocket.Parser.Interfaces;

namespace Rocket.Parser.Jobs
{
    /// <summary>
    /// Джоба для парсинга сайта lostfilm.tv
    /// </summary>
    [DisallowConcurrentExecution]
    internal class LostfilmParseJob : IJob
    {
        /// <summary>
        /// Парсит Lostfilm.
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            //todo логирование парсер запущен
            try
            {
                var schedulerContext = context.JobDetail.JobDataMap;
                var kernel = (IKernel)schedulerContext.Get(CommonHelper.ContainerKey);

                var lostfilmParseService = kernel.Get<ILostfilmParseService>();
                lostfilmParseService.ParseAsync();
            }
            catch (Exception excpt)
            {
                //todo логирование
                throw excpt;
            }

            //todo логирование парсер отработал
        }
    }
}