using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using Rocket.Parser.Models;

namespace Rocket.Parser.Interfaces
{
    public interface ILostfilmParseService
    {
        /// <summary>
        /// Парсим заголовок сериала из списка сериалов.
        /// </summary>
        /// <param name="serialTopElement">Элемент заголовка сериала.</param>
        /// <param name="lostfilmSerialModel">Модель для временной агрегации данных результата парсинга.</param>
        /// <param name="i">Счетчик.</param>
        void ParseSerialHeaderBase(IElement serialTopElement, LostfilmSerialModel lostfilmSerialModel, int i);

        /// <summary>
        /// Парсим заголовок сериала из списка сериалов панель детализации.
        /// </summary>
        /// <param name="serialTopElement">Элемент заголовка сериала.</param>
        /// <param name="lostfilmSerialModel">Модель для временной агрегации данных результата парсинга.</param>
        /// <param name="i">Счетчик.</param>
        void ParseSerialHeaderDetailsPane(IElement serialTopElement, LostfilmSerialModel lostfilmSerialModel, int i);

        string GetDetailsElement(string detailsText, string keyword, string endString);
        void ParseSerialOverviewDetails(IHtmlDocument htmlDocumentSerialListDetail, LostfilmSerialModel lostfilmSerialModel);
        void ParseOverviewNewSeria(IHtmlDocument htmlDocumentSerialOverviewDetails, LostfilmSerialModel lostfilmSerialModel);
        void ParseDetailNewSeria(IHtmlDocument htmlDetailNewSeria, LostfilmSerialModel lostfilmSerialModel);
    }
}