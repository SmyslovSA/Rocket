﻿using System;
using Ninject;
using Quartz;
using Rocket.Parser.Interfaces;

namespace Rocket.Parser.Jobs
{   
    /// <summary>
    /// Джоба для парсинга сайта album-info.ru
    /// </summary>
    [DisallowConcurrentExecution]
    internal class AlbumInfoParseJob : IJob
    {
        /// <summary>
        /// Запуск парсинга сайта album-info.ru 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            //todo логирование парсер запущен
            try
            {
                var schedulerContext = context.JobDetail.JobDataMap;
                var kernel = (IKernel)schedulerContext.Get("container");

                var parser = kernel.Get<IAlbumInfoParser>();
                parser.Parse();
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
