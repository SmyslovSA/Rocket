using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Rocket.Parser.Interfaces;
using Rocket.Parser.Models;

namespace Rocket.Parser.Parsers
{
    /// <summary>
    /// Парсер для сайта lostfilm.tv
    /// </summary>
    public class LostfilmParser: ILostfilmParser
    {
        private readonly ILoadHtmlService _loadHtmlService;
        private readonly ILostfilmParseService _lostfilmParseService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="loadHtmlService">Сервис загрузки HTML</param>

        public LostfilmParser(ILoadHtmlService loadHtmlService, ILostfilmParseService lostfilmParseService)
        {
            _loadHtmlService = loadHtmlService;
            _lostfilmParseService = lostfilmParseService;
        }

        public async Task<int> ParseAsync()
        {
            try
            {
                //todo эта настройка должна лежать в базе и задаваться через админку на UI, а пока в конфиге
                //Получаем основную ссылку на ресурс LostFilm
                var baseUrl = System.Configuration.ConfigurationManager.AppSettings["LostfilmParseBaseUrl"];

                //Получаем элемент со списком сериалов
                var htmlDocumentSerialList = await _loadHtmlService.GetHtmlDocumentByUrlAsync(baseUrl + "/series");
                var elSerialList = htmlDocumentSerialList.QuerySelector("#serials_list");

                //Формируем список моделей с данными которые нам удалось вытянуть с Lostfilm (шрузит по 10 сериалов за раз)
                var listLostfilmSerialModel = new List<LostfilmSerialModel>();
                int i = 2;
                IElement serialTopHtml;
                do
                {
                    //Перебираем сериалы пока не закончились в подгруженном списке
                    serialTopHtml = elSerialList.QuerySelector($"#serials_list > div:nth-child({i})");
                    if (serialTopHtml == null) break;

                    var lostfilmSerialModel = new LostfilmSerialModel();

                    //Парсим заголовок сериала из списка сериалов
                    _lostfilmParseService.ParseSerialHeaderBase(serialTopHtml, lostfilmSerialModel, i);

                    //Парсим заголовок сериала из списка сериалов панель детализации
                    _lostfilmParseService.ParseSerialHeaderDetailsPane(serialTopHtml, lostfilmSerialModel, i);

                    //Получаем элемент с обзорной информацией о сериале
                    var htmlDocumentSerialOverviewDetails =
                        await _loadHtmlService.GetHtmlDocumentByUrlAsync(baseUrl + lostfilmSerialModel.AddUrlForDetail);

                    //Парсим обзорную информацию по сериалу
                    _lostfilmParseService.ParseSerialOverviewDetails(htmlDocumentSerialOverviewDetails, lostfilmSerialModel);

                    //Парсим обзорную информации по еще не вышедшей серии
                    _lostfilmParseService.ParseOverviewNewSeria(htmlDocumentSerialOverviewDetails, lostfilmSerialModel);

                    if (lostfilmSerialModel.NewSeriaDetailNewUrl != null)
                    {
                        //Получаем элемент с более подробной информацией о новой серии
                        var htmlDetailNewSeria = await _loadHtmlService.GetHtmlDocumentByUrlAsync(baseUrl + lostfilmSerialModel.NewSeriaDetailNewUrl);

                        //Парсим информацию о новой серии с её страницы
                        _lostfilmParseService.ParseDetailNewSeria(htmlDetailNewSeria, lostfilmSerialModel);
                    }

                    listLostfilmSerialModel.Add(lostfilmSerialModel);

                    i = i + 2;
                }
                while (true || i < 2000); //на всякий случай предусмотрим выход из цикла после обработки 1000 сериалов

                //todo сделать чтобы догрузилось еще 10 строк

                //todo сделать запихивание данных в бд

                //todo сделать получение Person


                var t = 1211;

                return 1;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
