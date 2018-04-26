using System;
using System.Reflection;
using Ninject;
using Quartz;
using Rocket.Parser.Interfaces;

namespace Rocket.Parser.Jobs
{   
    /// <summary>
    /// Джоба для парсинга сайта album-info.ru
    /// </summary>
    [DisallowConcurrentExecution]
    internal class ParseAlbumInfoJob : IJob
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
                var kernel = new StandardKernel();
                kernel.Load(Assembly.GetExecutingAssembly());
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
