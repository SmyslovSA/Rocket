using System.Collections.Generic;

namespace Rocket.BL.Common.Models.Pagination
{
    /// <summary>
    /// Представляет модель постраничного просмотра коллекций
    /// </summary>
    /// <typeparam name="T">Тип элементов коллекции</typeparam>
    public class PageInfo<T>
    {
        /// <summary>
        /// Возвращает или задает номер страницы
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Возвращает или задает коллекцию элементов страницы
        /// </summary>
        public ICollection<T> PageItems { get; set; }

        /// <summary>
        /// Возвращает или задает размер страницы
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Возвращает или задает количество всех страниц
        /// </summary>
        public int TotalPagesCount { get; set; }

        /// <summary>
        /// Возвращает или задает количество всех элементов
        /// </summary>
        public int TotalItemsCount { get; set; }
    }
}
