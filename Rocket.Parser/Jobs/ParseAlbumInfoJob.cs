using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using Quartz;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.Repositories;
using Rocket.Parser.Interfaces;
using Rocket.Parser.Models;
using Rocket.Parser.Services;

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
                var loadHtmlService = new LoadHtmlService();
                var parseAlbumInfoService = new ParseAlbumInfoService(loadHtmlService, null);
                parseAlbumInfoService.Parse();

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
