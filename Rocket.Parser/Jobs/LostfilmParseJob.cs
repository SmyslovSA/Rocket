﻿using System;
using System.Threading.Tasks;
using Quartz;
using Rocket.Parser.Services;

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
                var lostfilmParseService = new ParseLostfilmService();

                var test = Task.Run(() => lostfilmParseService.Parse()).Result;
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