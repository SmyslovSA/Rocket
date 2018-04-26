﻿using System;
using System.Reflection;
using Ninject;
using Quartz;
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
                var kernel = new StandardKernel();
                kernel.Load(Assembly.GetExecutingAssembly());

                var parser = kernel.Get<ILostfilmParser>();
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