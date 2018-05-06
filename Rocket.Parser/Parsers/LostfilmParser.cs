using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.Parser.Extensions;
using Rocket.Parser.Heplers;
using Rocket.Parser.Interfaces;


namespace Rocket.Parser.Parsers
{
    /// <summary>
    /// Парсер Lostfilm
    /// </summary>
    internal class LostfilmParser : ILostfilmParser
    {
        private readonly ILoadHtmlService _loadHtmlService;

        private readonly string _baseUrl;
        private readonly int _maxRequestCount;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="loadHtmlService">Сервис для загрузки html</param>
        public LostfilmParser(ILoadHtmlService loadHtmlService)
        {
            _loadHtmlService = loadHtmlService;

            //Получаем базовую ссылку
            _baseUrl = LostfilmHelper.GetBaseUrl(); //todo эта настройка должна лежать в базе в админке
            //Получаем максимальное кол-во запросов к сайту
            int.TryParse(LostfilmHelper.GetMaxRequestCount(), out _maxRequestCount);
        }

        /// <summary>
        /// Базовая модель агрегации для сериала.
        /// </summary>
        private class TvSeriasAgregateModelBase
        {
            /// <summary>
            /// Сущность модели хранения данных о сериале.
            /// </summary>
            public TvSeriasEntity TvSeriasEntity { get; set; } = new TvSeriasEntity();

            /// <summary>
            /// Html подробной информации о сериале.
            /// </summary>
            public IHtmlDocument TvSeriasOverviewDetailsHtmlDoc { get; set; }
        }

        /// <summary>
        /// Расширенная модель агрегации для сериала.
        /// </summary>
        private class TvSeriasAgregateModelExt : TvSeriasAgregateModelBase
        {
            /// <summary>
            /// Html всех серий сериала.
            /// </summary>
            public IHtmlDocument TvSerialAllEpisodesHtmlDoc { get; set; }

            /// <summary>
            /// Html всех актеров, режисеров, продюсеров, сценаристов сериала.
            /// </summary>
            public IHtmlDocument TvSerialCastHtmlDoc { get; set; }
        }

        /// <summary>
        /// Парсим сайт Lostfilm
        /// </summary>
        public async Task ParseAsync()
        {
            try
            {
                var dtStart = DateTime.Now;

                //Получаем полный список элементов "список сериалов".
                var listTvSeriasListElement = LoadListTvSeriasListElementAll();

                //Парсим все элементы "список сериалов" и создаем агрегационные модели сериалов.
                var listTvSeriasAgregateModelBase = ParseSerialListElementAll(listTvSeriasListElement);

                int skip = 0;
                int countAll = listTvSeriasAgregateModelBase.Count;
                for (int i = 0; i < countAll; i += _maxRequestCount)
                {
                    //Создаем частичный список расширенных агрегационных моделей.
                    var listTvSeriasAgregateModelExt = 
                        CreateListTvSeriasAgregateModelExt(listTvSeriasAgregateModelBase, skip);
                    skip += _maxRequestCount;

                    //Загружаем html обзорной информации по сериалу в агрегационную модель.
                    LoadTvSeriasOverviewDetailsHtml(listTvSeriasAgregateModelExt);

                    //Парсим обзорную информацию по сериалу
                    Parallel.ForEach(listTvSeriasAgregateModelExt, ParseTvSeriasOverviewDetails);

                    //Загружаем html со списком всех серий в агрегационную модель.
                    LoadTvSeriasAllEpisodesHtml(listTvSeriasAgregateModelExt);

                    //Парсим все серии сериала.
                    Parallel.ForEach(listTvSeriasAgregateModelExt, ParseTvSeriasAllEpisodes);

                    // Загружаем актерский состав сериала.
                    LoadCastTvSeriasHtml(listTvSeriasAgregateModelExt);

                    //Парсим весь актерский и режисерский состав.
                    Parallel.ForEach(listTvSeriasAgregateModelExt, ParseCastTvSerias);

                    //todo сделать запихивание данных в бд
                }

                var duration = DateTime.Now - dtStart;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        /// <summary>
        /// Получаем полный список элементов "список сериалов".
        /// </summary>
        /// <returns>Cписок элементов "список сериалов".</returns>
        private List<IElement> LoadListTvSeriasListElementAll()
        {
            var listTvSeriasListElementAll = new List<IElement>();

            //Получаем текст сообщения достижения конца списка сериалов на сайте
            string messageNotFoundByRequest = LostfilmHelper.GetMessageNotFoundByRequest();
            //Получаем кол-во сериалов которые сайт выдает за раз
            int.TryParse(LostfilmHelper.GetTakeTvSeriasByRequest(), out int takeTvSeriasByRequest);

            List<IElement> listTvSeriasListElementBatch;
            int getTvSeriasListIteration = 0;
            do
            {
                //Получаем частичный список элементов "список сериалов" с сайта.
                listTvSeriasListElementBatch = LoadListTvSeriasListElementBatch(ref getTvSeriasListIteration,
                    messageNotFoundByRequest, takeTvSeriasByRequest);

                //Добавляем в общий список
                listTvSeriasListElementAll.AddRange(listTvSeriasListElementBatch);

                //Выполняем до тех пор пока сайт возвращает нам элементы "список сериалов"
            } while (listTvSeriasListElementBatch.Any());

            return listTvSeriasListElementAll;
        }

        /// <summary>
        /// Получаем частичный список элементов "список сериалов" с сайта.
        /// </summary>
        /// <param name="getTvSeriasListIteration">Итерация получения элемента "список сериалов" с сайта.</param>
        /// <param name="messageNotFoundByRequest">Сообщение что мы достигли конца списка сериалов.</param>
        /// <param name="takeTvSeriasByRequest">Кол-во сериалов которые сайт отдает за раз.</param>
        /// <returns>Частичный список элементов "список сериалов" с сайта.</returns>
        private List<IElement> LoadListTvSeriasListElementBatch(ref int getTvSeriasListIteration,
            string messageNotFoundByRequest, int takeTvSeriasByRequest)
        {
            var listTvSeriasListBatchElement = new List<IElement>();

            //Формируем список тасок на частичное получение списка элементов "список сериалов" с сайта
            var listTaskGetTvSeriasListElement = new Task<IElement>[_maxRequestCount];
            for (int taskCounter = 0; taskCounter < _maxRequestCount; taskCounter++)
            {
                var taskGetTvSeriasListElement =
                    LoadTvSeriasListElement(getTvSeriasListIteration + (taskCounter + 1) * takeTvSeriasByRequest);
                listTaskGetTvSeriasListElement[taskCounter] = taskGetTvSeriasListElement;
            }

            //Дожидаемся выполнение тасок
            Task.WaitAll(listTaskGetTvSeriasListElement.ToArray());

            //Формируем частичный список элементов "список сериалов" с сайта
            foreach (var taskGetTvSeriasListElement in listTaskGetTvSeriasListElement)
            {
                var tvSeriasListBatchElement = taskGetTvSeriasListElement.Result;

                if (tvSeriasListBatchElement.InnerHtml.IndexOf(messageNotFoundByRequest, StringComparison.Ordinal) < 0)
                {
                    listTvSeriasListBatchElement.Add(tvSeriasListBatchElement);
                }
            }

            getTvSeriasListIteration = getTvSeriasListIteration + takeTvSeriasByRequest * _maxRequestCount;

            return listTvSeriasListBatchElement;
        }

        /// <summary>
        /// Получаем список сериалов пачками по 10 штук(больше сайт не отдает за раз) с проверкой доступности сайта.
        /// </summary>
        /// <param name="getSerialListIteration">Итерация получения списка сериалов.</param>
        /// <returns>Элемент со списком сериалов.</returns>
        private async Task<IElement> LoadTvSeriasListElement(int getSerialListIteration)
        {
            try
            {
                //Получаем элемент со списком сериалов
                var htmlDocumentSerialList = await _loadHtmlService.GetHtmlDocumentByUrlAsync(
                    _baseUrl + LostfilmHelper.GetAdditionalUrlToSerialList() +
                    string.Format(LostfilmHelper.GetAdditionalUrlToSerialBatchList(), getSerialListIteration));

                return htmlDocumentSerialList.QuerySelector(LostfilmHelper.GetTvSerailListHeaderBase());
            }
            catch (Exception e)
            {
                //todo запись в лог о неудачной попытке обратиться к сайту
                throw e;
            }
        }

        /// <summary>
        /// Парсим все элементы "список сериалов" и создаем агрегационные модели сериалов.
        /// </summary>
        /// <param name="listTvSeriasListElement">Список элементов "список сериалов".</param>
        /// <returns>Список агрегационных моделей сериалов.</returns>
        private List<TvSeriasAgregateModelBase> ParseSerialListElementAll(List<IElement> listTvSeriasListElement)
        {
            var listTvSeriasAgregateModelBase = new List<TvSeriasAgregateModelBase>();

            foreach (var tvSeriasListElement in listTvSeriasListElement)
            {
                listTvSeriasAgregateModelBase.AddRange(ParseSerialListElement(tvSeriasListElement));
            }

            return listTvSeriasAgregateModelBase;
        }

        /// <summary>
        /// Парсим элемент "список сериалов" и создаем агрегационные модели сериалов.
        /// </summary>
        /// <param name="tvSeriasListElement">Элемент "список сериалов".</param>
        /// <returns>Список агрегационных моделей сериалов.</returns>
        private List<TvSeriasAgregateModelBase> ParseSerialListElement(IElement tvSeriasListElement)
        {
            var listTvSeriasAgregateModel = new List<TvSeriasAgregateModelBase>();

            //Получаем кол-во сериалов которые сайт выдает за раз
            int.TryParse(LostfilmHelper.GetTakeTvSeriasByRequest(), out int takeTvSeriasByRequest);

            //Формируем список тасок на выполнение
            var listTaskParseTvSeriasHeader = new List<Task<TvSeriasAgregateModelBase>>();
            int step = 2;
            for (int i = step; i <= takeTvSeriasByRequest * step; i += step)
            {
                int selectorIterator = i;
                var taskParseTvSeriasHeader = 
                    Task.Run(() => ParseTvSeriasHeader(tvSeriasListElement, selectorIterator));
                listTaskParseTvSeriasHeader.Add(taskParseTvSeriasHeader);
            }

            //Запускаем таски на выполнение
            Task.WaitAll(listTaskParseTvSeriasHeader.ToArray());

            //Получаем список сущностей сериалов
            foreach (var taskParseTvSeriasHeader in listTaskParseTvSeriasHeader)
            {
                var tvSeriasAgregateModel = taskParseTvSeriasHeader.Result;
                if (tvSeriasAgregateModel != null) listTvSeriasAgregateModel.Add(tvSeriasAgregateModel);
            }

            return listTvSeriasAgregateModel;
        }

        /// <summary>
        /// Парсим заголовок сериала из списка сериалов.
        /// </summary>
        /// <param name="elSerialList">Элемент список заголовков сериалов.</param>
        /// <param name="selectorIterator">Счетчик.</param>
        /// <returns>Агрегационная модель сериала.</returns>
        private TvSeriasAgregateModelBase ParseTvSeriasHeader(IElement elSerialList, int selectorIterator)
        {
            var tvSeriasEntity = new TvSeriasEntity();

            var serialTopElement = elSerialList.QuerySelector(
                string.Format(LostfilmHelper.GetTvSerialHeader(), selectorIterator));
            if (serialTopElement == null) return null;

            //Получаем элемент детализации по сериалу из заголовка
            var addUrlForDetailElement = serialTopElement
                .QuerySelector(string.Format(LostfilmHelper.GetTvSerialHeaderDetail(), selectorIterator));

            //Получаем дополнительную ссылку для получения деталей по сериалу.
            tvSeriasEntity.UrlToSource = _baseUrl + addUrlForDetailElement.GetAttribute(CommonHelper.HrefAttribute);

            //todo сделать проверку на наличие сериала в бд по ссылке, если есть код ниже выполняться не должен
            //todo кроме обновление рейтинга и текущего статуса сериала!!!

            //Получаем ссылку на изображение-миниатюру для сериала.
            var imageUrlTvSerialThumbElement = serialTopElement.QuerySelector(
                string.Format(LostfilmHelper.GetTvSerialHeaderDetailImageUrlThumb(), selectorIterator));
            tvSeriasEntity.PosterImageUrl =
                CommonHelper.HttpText + imageUrlTvSerialThumbElement.GetAttribute(CommonHelper.SrcAttribute);

            //Получаем название сериала по-русски.
            var tvSerialNameRuElement = serialTopElement.QuerySelector(
                    string.Format(LostfilmHelper.GetTvSerialHeaderDetailTvSerialNameRu(), selectorIterator));
            tvSeriasEntity.TitleRu = tvSerialNameRuElement?.InnerHtml;

            //Получаем название сериала по-английски.
            var tvSerialNameEnElement = serialTopElement.QuerySelector(
                string.Format(LostfilmHelper.GetTvSerialHeaderDetailTvSerialNameEn(), selectorIterator));
            tvSeriasEntity.TitleEn = tvSerialNameEnElement?.InnerHtml;

            //Получаем рейтинг сериала на Lostfilm.
            var lostfilmRateElement = serialTopElement.QuerySelector(
                string.Format(LostfilmHelper.GetTvSerialHeaderLostfilmRate(), selectorIterator));
            double.TryParse(lostfilmRateElement.InnerHtml, out double lostfilmRate);
            tvSeriasEntity.LostfilmRate = lostfilmRate;

            //Парсим заголовок сериала из списка сериалов панель детализации
            ParseTvSeriasHeaderDetailsPane(serialTopElement, tvSeriasEntity, selectorIterator);

            var tvSeriasAgregateModel = new TvSeriasAgregateModelBase { TvSeriasEntity = tvSeriasEntity };

            return tvSeriasAgregateModel;
        }

        /// <summary>
        /// Парсим заголовок сериала из списка сериалов панель детализации.
        /// </summary>
        /// <param name="serialTopElement">Элемент заголовка сериала.</param>
        /// <param name="tvSeriasEntity"></param>
        /// <param name="selectorIterator">Счетчик елементов заголовка сериала.</param>
        private void ParseTvSeriasHeaderDetailsPane(IElement serialTopElement,
            TvSeriasEntity tvSeriasEntity, int selectorIterator)
        {
            //Получаем панель деталей
            var detailsPaneElement = serialTopElement.QuerySelector(
                string.Format(LostfilmHelper.GetTvSerialHeaderDetailPane(), selectorIterator));
            string detailsPane = detailsPaneElement.InnerHtml;

            //Получаем текущий статус сериала
            tvSeriasEntity.CurrentStatus = detailsPane.GetSubstring(
                LostfilmHelper.GetTvSerialHeaderKeywordStatus(), CommonHelper.OpenAngleBracket);

            //Получаем теливизионный канал на котором показывают сериал
            tvSeriasEntity.TvSerialCanal = detailsPane.GetSubstring(
                LostfilmHelper.GetTvSerialHeaderKeywordCanal(), CommonHelper.OpenAngleBracket);

            //Получаем список жанров в виде строки для последующего парсинга.
            tvSeriasEntity.ListGenreForParse = detailsPane.GetSubstring(
                LostfilmHelper.GetTvSerialHeaderKeywordGenre(), CommonHelper.OpenAngleBracket);

            //Получаем год начала показа сериала.
            tvSeriasEntity.TvSerialYearStart = detailsPane.GetSubstring(
                LostfilmHelper.GetTvSerialHeaderKeywordYearStart(), CommonHelper.OpenAngleBracket);
        }

        /// <summary>
        /// Создаем частичный список расширенных агрегационных моделей.
        /// </summary>
        /// <param name="listTvSeriasAgregateModelBase">Список базовых агрегационных моделей.</param>
        /// <param name="skip">Кол-во пропускать.</param>
        /// <returns>Список расширенных агрегационных моделей.</returns>
        private List<TvSeriasAgregateModelExt> CreateListTvSeriasAgregateModelExt(
            List<TvSeriasAgregateModelBase> listTvSeriasAgregateModelBase, int skip)
        {
            var listTvSeriasAgregateModelExt = new List<TvSeriasAgregateModelExt>();

            var listTvSeriasAgregateModelBaseBatch = listTvSeriasAgregateModelBase
                .Skip(skip)
                .Take(_maxRequestCount)
                .ToList();

            foreach (var tvSeriasAgregateModelBase in listTvSeriasAgregateModelBaseBatch)
            {
                var tvSeriasAgregateModelExt = new TvSeriasAgregateModelExt
                {
                    TvSeriasEntity = tvSeriasAgregateModelBase.TvSeriasEntity,
                    TvSeriasOverviewDetailsHtmlDoc = tvSeriasAgregateModelBase.TvSeriasOverviewDetailsHtmlDoc
                };

                listTvSeriasAgregateModelExt.Add(tvSeriasAgregateModelExt);
            }

            return listTvSeriasAgregateModelExt;
        }

        /// <summary>
        /// Загружаем html обзорной информации по сериалу в агрегационную модель.
        /// </summary>
        /// <param name="listTvSeriasAgregateModel">Список агрегационных моделей.</param>
        private void LoadTvSeriasOverviewDetailsHtml(List<TvSeriasAgregateModelExt> listTvSeriasAgregateModel)
        {
            //Получаем список Url для загрузки
            var listUrl = listTvSeriasAgregateModel
                .Select(item => item.TvSeriasEntity.UrlToSource)
                .ToList();

            //Получаем список html моделей
            var listHtmlDocumentModel = 
                _loadHtmlService.GetListHtmlDocumentModel(listUrl, listTvSeriasAgregateModel.Count);

            //Устанавливаем связку сериала и 
            foreach (var tvSeriasAgregateModel in listTvSeriasAgregateModel)
            {
                tvSeriasAgregateModel.TvSeriasOverviewDetailsHtmlDoc = 
                    listHtmlDocumentModel
                        .First(item => item.Url == tvSeriasAgregateModel.TvSeriasEntity.UrlToSource)
                        .HtmlDocument;
            }
        }

        /// <summary>
        /// Парсим обзорную информацию по сериалу
        /// </summary>
        /// <param name="tvSeriasAgregateModel">Агрегационная модель.</param>
        private void ParseTvSeriasOverviewDetails(TvSeriasAgregateModelExt tvSeriasAgregateModel)
        {
            var tvSeriasEntity = tvSeriasAgregateModel.TvSeriasEntity;

            var serialOverviewElement = tvSeriasAgregateModel.TvSeriasOverviewDetailsHtmlDoc
                .QuerySelector(LostfilmHelper.GetTvSerialOverview());

            var serialOverviewText = serialOverviewElement.InnerHtml;

            //Получаем рейтинг ImDb
            string rateImDbText = serialOverviewText.GetSubstring(LostfilmHelper.GetTvSerialKeywordRateImDb(), CommonHelper.OpenAngleBracket);

            double.TryParse(rateImDbText, out double rateImDb);
            tvSeriasEntity.RateImDb = rateImDb;

            //Получаем дату премьеры сериала прописью
            var premiereDateForParseElement = serialOverviewElement.QuerySelector(
                LostfilmHelper.GetTvSerialPremiereDateForParse());
            tvSeriasEntity.PremiereDateForParse = premiereDateForParseElement?.InnerHtml; //todo распарсить

            //Получаем ссылку на ориганильный сайт
            var urlToOfficialSiteElement =
                serialOverviewElement.QuerySelector(LostfilmHelper.GetTvSerialUrlToOfficialSite());
            tvSeriasEntity.UrlToOfficialSite = urlToOfficialSiteElement?.InnerHtml;

            //Полчаем описание сериала
            var summaryElement = serialOverviewElement.QuerySelector(LostfilmHelper.GetTvSerialSummary());
            tvSeriasEntity.Summary = summaryElement?.InnerHtml;
            //todo доработать чтобы тут было только описание

            //todo страна

        }

        /// <summary>
        /// Загружаем html со списком всех серий в агрегационную модель.
        /// </summary>
        /// <param name="listTvSeriasAgregateModel">Список агрегационных моделей.</param>
        private void LoadTvSeriasAllEpisodesHtml(List<TvSeriasAgregateModelExt> listTvSeriasAgregateModel)
        {
            string additionalUrlToTvSerialEpisodes = LostfilmHelper.AdditionalUrlToTvSerialEpisodes();

            //Получаем список Url для загрузки
            var listUrl = listTvSeriasAgregateModel
                .Select(item => item.TvSeriasEntity.UrlToSource + additionalUrlToTvSerialEpisodes)
                .ToList();

            //Получаем список html моделей
            var listHtmlDocumentModel = _loadHtmlService.GetListHtmlDocumentModel(listUrl, _maxRequestCount);

            //Устанавливаем связку сериала и html страница со всеми сериями сериала.
            foreach (var tvSeriasAgregateModel in listTvSeriasAgregateModel)
            {
                tvSeriasAgregateModel.TvSerialAllEpisodesHtmlDoc =
                    listHtmlDocumentModel
                        .First(item => 
                            item.Url == tvSeriasAgregateModel.TvSeriasEntity.UrlToSource + additionalUrlToTvSerialEpisodes)
                        .HtmlDocument;
            }
        }

        /// <summary>
        /// Парсим все серии и сериала.
        /// </summary>
        /// <param name="tvSeriasAgregateModel">Агрегационная модель.</param>
        private void ParseTvSeriasAllEpisodes(TvSeriasAgregateModelExt tvSeriasAgregateModel)
        {
            try
            {
                var tvSeriasEntity = tvSeriasAgregateModel.TvSeriasEntity;

                var tvSerialAllEpisodesElement = tvSeriasAgregateModel.TvSerialAllEpisodesHtmlDoc
                    .QuerySelector(LostfilmHelper.GetAllEpisodes());

                //Получает массив постеров сезонов.
                var arrSeasonPosters = GetSeasonPosters(tvSerialAllEpisodesElement);

                //Получаем список всех эпизодов
                var listTvSerialAllEpisodes =
                    tvSerialAllEpisodesElement.GetElementsByTagName(CommonHelper.TrAttribute);

                foreach (var episodeElement in listTvSerialAllEpisodes)
                {
                    //Получаем номер сериала и номер сезона.
                    if (!GetSerialNumberAndEpisodeNumber(episodeElement, out int seasonsNumber, out int episodeNumber)) continue;

                    //Получаем сущность сезона.
                    var seasonEntity = GetSeasonEntity(tvSeriasEntity, seasonsNumber, arrSeasonPosters);

                    //Получаем сущность серии
                    var episodeEntity = GetEpisodeEntity(tvSeriasEntity, seasonsNumber, episodeNumber);

                    //Получаем ссылку на серию.
                    GetUrlForEpisodeSource(episodeElement, episodeEntity);

                    //Получаем название серии (русское и английское).
                    GetEpisodeName(episodeElement, episodeEntity);

                    //Получаем даты релизов (русская версия и английская).
                    GetReleaseDate(episodeElement, episodeEntity);

                    seasonEntity.ListEpisode.Add(episodeEntity);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

        }

        /// <summary>
        /// Получает массив постеров сезонов.
        /// </summary>
        /// <param name="tvSerialAllEpisodesElement">Элемент содержащий все серии.</param>
        /// <returns>Массив постеров сезонов.</returns>
        private string[] GetSeasonPosters(IElement tvSerialAllEpisodesElement)
        {
            var seasonPosters = tvSerialAllEpisodesElement.GetElementsByClassName(LostfilmHelper.GetSeasonPosters());
            int countSeasons = seasonPosters.Length;
            string[] arrSeasonPosters = new string[seasonPosters.Length];
            int i = 1;
            foreach (var seasonPoster in seasonPosters)
            {
                arrSeasonPosters[countSeasons - i] =
                    CommonHelper.HttpText + seasonPoster.OuterHtml.GetSubstring(CommonHelper.Apostrophe, CommonHelper.Apostrophe);
                i++;
            }

            return arrSeasonPosters;
        }

        /// <summary>
        /// Получаем сущность сезона.
        /// </summary>
        /// <param name="seasonsNumber">Номер сезона</param>
        /// <param name="arrSeasonPosters">массив постеров сезонов.</param>
        /// <returns>сущность сезона.</returns>
        private SeasonEntity GetSeasonEntity(TvSeriasEntity tvSeriasEntity, int seasonsNumber, string[] arrSeasonPosters)
        {
            var seasonEntity = tvSeriasEntity.ListSeasons
                .FirstOrDefault(item => item.Number == seasonsNumber);
            if (seasonEntity == null)
            {
                seasonEntity = new SeasonEntity
                {
                    ListEpisode = new List<EpisodeEntity>(),
                    Number = seasonsNumber
                };

                if (arrSeasonPosters.Length >= seasonsNumber)
                {
                    seasonEntity.PosterImageUrl = arrSeasonPosters[seasonsNumber - 1];
                }

                tvSeriasEntity.ListSeasons.Add(seasonEntity);
            }

            return seasonEntity;
        }

        /// <summary>
        /// Получаем номер сериала и номер сезона.
        /// </summary>
        /// <param name="episodeElement">Элемент серии.</param>
        /// <param name="seasonsNumber">Номер сезона(возвращаемое).</param>
        /// <param name="episodeNumber">Номер сериала(возвращаемое).</param>
        private bool GetSerialNumberAndEpisodeNumber(IElement episodeElement, out int seasonsNumber,
            out int episodeNumber)
        {
            seasonsNumber = 0;
            episodeNumber = 0;

            var episodeAndSeriaNumberForParse = episodeElement
                .GetElementsByClassName(LostfilmHelper.GetEpisodeClassNameBeta()).FirstOrDefault()?.InnerHtml;
            if(string.IsNullOrEmpty(episodeAndSeriaNumberForParse)) return false;

            var seasonNumberText = episodeAndSeriaNumberForParse.GetSubstring(string.Empty, LostfilmHelper.GetSeasonKeyword());
            var episodeNumberText = episodeAndSeriaNumberForParse.GetSubstring(LostfilmHelper.GetSeasonKeyword(), LostfilmHelper.GetEpisodeKeyword());

            int.TryParse(seasonNumberText, out int seasonsNumberRes);
            int.TryParse(episodeNumberText, out int episodeNumberRes);

            seasonsNumber = seasonsNumberRes;
            episodeNumber = episodeNumberRes;

            return true;
        }

        /// <summary>
        /// Получаем сущность серии.
        /// </summary>
        /// <param name="seasonsNumber">Номер сезона</param>
        /// <param name="episodeNumber">Номер серии.</param>
        /// <returns>сущность серии.</returns>
        private EpisodeEntity GetEpisodeEntity(TvSeriasEntity tvSeriasEntity, int seasonsNumber, int episodeNumber)
        {
            var episodeEntity = tvSeriasEntity.ListSeasons
                .First(item => item.Number == seasonsNumber)
                .ListEpisode
                .FirstOrDefault(item => item.Number == episodeNumber);

            if (episodeEntity != null) return episodeEntity;

            episodeEntity = new EpisodeEntity
            {
                Number = episodeNumber
            };

            return episodeEntity;
        }

        /// <summary>
        /// Получаем ссылку на серию.
        /// </summary>
        /// <param name="episodeElement">Элемент серии.</param>
        /// <param name="episodeEntity">Сущность эпизода.</param>
        private void GetUrlForEpisodeSource(IElement episodeElement, EpisodeEntity episodeEntity)
        {
            string outerHtmlBetaClass = episodeElement
                .GetElementsByClassName(LostfilmHelper.GetEpisodeClassNameBeta()).First().OuterHtml;
            episodeEntity.UrlForEpisodeSource =
                LostfilmHelper.GetBaseUrl() + outerHtmlBetaClass.GetSubstring(CommonHelper.Apostrophe, CommonHelper.Apostrophe);
        }

        /// <summary>
        /// Получаем название серии (русское и английское).
        /// </summary>
        /// <param name="episodeElement">Элемент серии.</param>
        /// <param name="episodeEntity">Сущность эпизода.</param>
        private void GetEpisodeName(IElement episodeElement, EpisodeEntity episodeEntity)
        {
            var gammaClassElement = episodeElement
                .GetElementsByClassName(LostfilmHelper.GetEpisodeClassNameGamma()).First();
            string someTextForParseName = gammaClassElement.GetElementsByTagName(CommonHelper.DivAttribute)
                .FirstOrDefault()?.InnerHtml;
            if (!string.IsNullOrEmpty(someTextForParseName))
            {
                episodeEntity.TitleRu = someTextForParseName.GetSubstring(string.Empty, CommonHelper.OpenAngleBracket, false);
                episodeEntity.TitleEn = episodeElement
                    .GetElementsByClassName(LostfilmHelper.GetGrayColor2SmallTextClass())
                    .First()
                    .InnerHtml;
            }
            else
            {
                someTextForParseName = gammaClassElement.OuterHtml;
                episodeEntity.TitleRu = someTextForParseName.GetSubstring(CommonHelper.CloseAngleBracket, CommonHelper.BrAttribute + CommonHelper.CloseAngleBracket, false);
                episodeEntity.TitleEn = episodeElement
                    .GetElementsByClassName(LostfilmHelper.GetSmallTextClass())
                    .First()
                    .InnerHtml;
            }
        }

        /// <summary>
        /// Получаем даты релизов (русская версия и английская).
        /// </summary>
        /// <param name="episodeElement">Элемент серии.</param>
        /// <param name="episodeEntity">Сущность эпизода.</param>
        private void GetReleaseDate(IElement episodeElement, EpisodeEntity episodeEntity)
        {
            string someTextForParseDates = episodeElement
                .GetElementsByClassName(LostfilmHelper.GetEpisodeClassNameDelta())
                .First()
                .OuterHtml;

            string releaseDateRuText = someTextForParseDates.GetSubstring(
                LostfilmHelper.GetRuKeyword(), CommonHelper.OpenAngleBracket);
            DateTime.TryParseExact(releaseDateRuText, LostfilmHelper.GetDateFormat(),
                new CultureInfo(CommonHelper.CultureInfoText), DateTimeStyles.None, out DateTime releaseDateRu);
            episodeEntity.ReleaseDateRu = releaseDateRu;

            string releaseDateEnText = someTextForParseDates.GetSubstring(
                LostfilmHelper.GetEngKeyword(), CommonHelper.OpenAngleBracket);
            DateTime.TryParseExact(releaseDateEnText, LostfilmHelper.GetDateFormat(),
                new CultureInfo(CommonHelper.CultureInfoText), DateTimeStyles.None, out DateTime releaseDateEn);
            episodeEntity.ReleaseDateEn = releaseDateEn;
        }

        /// <summary>
        /// Загружаем актерский состав сериала.
        /// </summary>
        /// <param name="listTvSeriasAgregateModel">Агрегационная модель.</param>
        private void LoadCastTvSeriasHtml(List<TvSeriasAgregateModelExt> listTvSeriasAgregateModel)
        {
            string additionalUrlToTvSerialCasts = LostfilmHelper.AdditionalUrlToTvSerialCasts();

            //Получаем список Url для загрузки
            var listUrl = listTvSeriasAgregateModel
                .Select(item => item.TvSeriasEntity.UrlToSource + additionalUrlToTvSerialCasts)
                .ToList();

            //Получаем список html моделей
            var listHtmlDocumentModel = _loadHtmlService.GetListHtmlDocumentModel(listUrl, _maxRequestCount);

            //Устанавливаем связку сериала и html страница со всем актерским составом.
            foreach (var tvSeriasAgregateModel in listTvSeriasAgregateModel)
            {
                tvSeriasAgregateModel.TvSerialCastHtmlDoc =
                    listHtmlDocumentModel
                        .First(item =>
                            item.Url == tvSeriasAgregateModel.TvSeriasEntity.UrlToSource + additionalUrlToTvSerialCasts)
                        .HtmlDocument;
            }
        }

        /// <summary>
        /// Парсим весь актерский и режисерский состав.
        /// </summary>
        /// <param name="tvSeriasAgregateModel">Модель агрегации сериала.</param>
        private void ParseCastTvSerias(TvSeriasAgregateModelExt tvSeriasAgregateModel)
        {
            var htmlDocumentCastTvSerias = tvSeriasAgregateModel.TvSerialCastHtmlDoc;
            
            var listCastElement = new List<IElement>();
            int i = 1;
            string castType = string.Empty;
            do
            {
                //Получаем элемент актерского состава.
                var castElement = GetCastElement(htmlDocumentCastTvSerias, i);
                if (castElement == null) break;

                listCastElement.Add(castElement);

                var tvSeriasEntity = tvSeriasAgregateModel.TvSeriasEntity;
                if (LostfilmHelper.GetClassRow() == castElement.ClassName)
                {
                    //Создаем сущность персоны.
                    var personEntity = CreatePersonEntity(castElement);

                    //Добавляем в сущность хранения данных о сериале персону.
                    AddPersonEntityToTvSerias(tvSeriasEntity, personEntity, castType);
                }
                else
                {
                    if (LostfilmHelper.GetClassHeaderSimple() == castElement.ClassName)
                    {
                        castType = castElement.InnerHtml;
                    }
                }

                i++;
            } while (true);
        }

        /// <summary>
        /// Получаем элемент актерского состава.
        /// </summary>
        /// <param name="htmlDocumentCastTvSerias">htmlDocument актерского состава.</param>
        /// <param name="i">Счетчик.</param>
        /// <returns>Элемент актерского состава.</returns>
        private IElement GetCastElement(IHtmlDocument htmlDocumentCastTvSerias, int i)
        {
            var castElement = htmlDocumentCastTvSerias.QuerySelector(
                string.Format(LostfilmHelper.GetSelectorCastElement(), i));

            if (castElement == null)
            {
                castElement = htmlDocumentCastTvSerias.QuerySelector(
                    string.Format(LostfilmHelper.GetSelectorHrefCastElement(), i));
            }

            return castElement;
        }

        /// <summary>
        /// Создаем сущность персоны.
        /// </summary>
        /// <param name="castElement">Элемент актерского состава.</param>
        /// <returns>Сущность персоны.</returns>
        private PersonEntity CreatePersonEntity(IElement castElement)
        {
            var personEntity = new PersonEntity();

            personEntity.LostfilmPersonalPageUrl =
                LostfilmHelper.GetBaseUrl() + castElement.GetAttribute(CommonHelper.HrefAttribute);

            var aloadThumbElement = castElement
                .GetElementsByClassName(LostfilmHelper.GetClassAloadThumb())
                .FirstOrDefault();

            if (aloadThumbElement != null)
            {
                personEntity.PhotoThumbnailUrl =
                    CommonHelper.HttpText + aloadThumbElement.GetAttribute(CommonHelper.AutoloadAttribute);
            }

            personEntity.FullNameRu = castElement
                .GetElementsByClassName(LostfilmHelper.GetClassNameRu())
                .First()
                .InnerHtml;
            personEntity.FullNameEn = castElement
                .GetElementsByClassName(LostfilmHelper.GetClassNameEn())
                .First()
                .InnerHtml;

            return personEntity;
        }

        /// <summary>
        /// Добавляем в сущность хранения данных о сериале персону.
        /// </summary>
        /// <param name="personEntity">Сущность персоны.</param>
        /// <param name="castType">Тип персоны.</param>
        private void AddPersonEntityToTvSerias(TvSeriasEntity tvSeriasEntity, PersonEntity personEntity,
            string castType)
        {
            //Если актер добавляем в список актеров
            if (castType == LostfilmHelper.GetKeywordActors())
            {
                tvSeriasEntity.ListActor.Add(personEntity);
                return;
            }

            //Если режисер добавляем в список режисеров
            if (castType == LostfilmHelper.GetKeywordDirectors())
            {
                tvSeriasEntity.ListDirector.Add(personEntity);
                return;
            }

            //Если продюссер добавляем в список продюссеров
            if (castType == LostfilmHelper.GetKeywordProducers())
            {
                tvSeriasEntity.ListProducer.Add(personEntity);
                return;
            }

            //Если сценарист добавляем в список сценаристов
            if (castType == LostfilmHelper.GetKeywordWriters())
            {
                tvSeriasEntity.ListWriter.Add(personEntity);
                return;
            }
        }
    }
}
