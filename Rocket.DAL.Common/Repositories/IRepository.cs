using System.Collections.Generic;

namespace Rocket.DAL.Common.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Получить список всех элементов
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> FetchAll();

        /// <summary>
        /// Получить элемент по ID элемента
        /// </summary>
        TEntity GetElementById(int key);

        /// <summary>
        /// Добавить элемент сущности
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Изменить элемент сущности
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Удалить элемент сущности
        /// </summary>
        void Delete(int key);
    }
}


