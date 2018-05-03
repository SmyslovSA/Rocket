﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rocket.DAL.Common.Repositories
{
    /// <summary>
    /// Представляет обобщенный репозиторий
    /// Код взят из статьи https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
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
    }

}