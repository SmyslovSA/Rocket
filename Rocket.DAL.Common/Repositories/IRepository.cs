using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rocket.DAL.Common.Repositories
{
    /// <summary>
    /// Представляет обобщенный репозиторий
    /// </summary>
    /// <typeparam name="TEntity">Тип, экземплярами которого управляет репозиторий</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Возвращает перечисление экземпляров <see cref="TEntity"/> из хранилища данных.
        /// Применяет фильтр, сортировку и загрузку связанных свойств,
        /// если заданы соответствующие значения параметров
        /// </summary>
        /// <param name="filter">Лямбда-выражение определяющее фильтрацию экземпляров <see cref="TEntity"/></param>
        /// <param name="orderBy">Лямбда-выражение определяющее сортировку экземпляров <see cref="TEntity"/></param>
        /// <param name="includeProperties">Список связанных свойств экземпляров <see cref="TEntity"/>, разделенный запятыми</param>
        /// <returns>Перечисление экземпляров <see cref="TEntity"/></returns>
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Возвращает экземпляр <see cref="TEntity"/>,
        /// соответствующий заданному идентификатору, из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор<see cref="TKey"/></param>
        /// <returns>Экземпляр <see cref="TEntity"/></returns>
        TEntity GetById<TKey>(TKey id);

        void SetStatusAdded(TEntity entity);

        void SetStatusAddedRange(IEnumerable<TEntity> entities);

        void SetStatusNotModified(TEntity entity);

        void SetStatusNotModifiedRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Добавляет заданный экземпляр <see cref="TEntity"/> в хранилище данных
        /// </summary>
        /// <param name="entity">Экземпляр <see cref="TEntity"/></param>
        void Insert(TEntity entity);

        /// <summary>
        /// Обновляет заданный экземпляр <see cref="TEntity"/> в хранилище данных
        /// </summary>
        /// <param name="entity">Экземпляр <see cref="TEntity"/></param>
        void Update(TEntity entity);

        /// <summary>
        /// Удаляет экземпляр <see cref="TEntity"/>,
        /// соответствующий заданному идентификатору, из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор<see cref="TKey"/></param>
        void Delete<TKey>(TKey id);

        /// <summary>
        /// Удаляет заданный экземпляр <see cref="TEntity"/> из хранилища данных
        /// </summary>
        /// <param name="entity">Экземпляр <see cref="TEntity"/></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Поиск по первичному ключу
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        TEntity Find<TKey>(params TKey[] keyValues);

        /// <summary>
        /// Получить выборку
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable<TEntity> SelectQuery<TKey>(string query, params TKey[] parameters);

        /// <summary>
        /// Вставка коллекции
        /// </summary>
        /// <param name="entities">Коллекция записей для вставки</param>
        void InsertRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Возвращает Queryable сущности
        /// </summary>
        /// <returns>IQueryable</returns>
        IQueryable<TEntity> Queryable();

        int SaveChanges();
    }
}