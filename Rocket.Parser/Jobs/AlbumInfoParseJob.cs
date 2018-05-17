﻿using System;
using Ninject;
using Quartz;
using Rocket.Parser.Heplers;
using Rocket.Parser.Interfaces;

namespace Rocket.Parser.Jobs
{   
    /// <inheritdoc />
    /// <summary>
    /// Джоба для парсинга сайта album-info.ru
    /// </summary>
    [DisallowConcurrentExecution]
    internal class AlbumInfoParseJob : IJob
    {
        /// <inheritdoc />
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
                var kernel = (IKernel)schedulerContext.Get(CommonHelper.ContainerKey);

                var parser = kernel.Get<IAlbumInfoParser>();
                parser.ParseAsync();
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