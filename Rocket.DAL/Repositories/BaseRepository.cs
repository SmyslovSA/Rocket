using Rocket.DAL.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Rocket.DAL.Repositories
{
    /// <summary>
    /// Представляет обобщенный репозиторий
    /// Код взят из статьи https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    /// </summary>
    /// <typeparam name="TEntity">Тип, экземплярами которого управляет репозиторий</typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private DbContext _dbContext;
        private IDbSet<TEntity> _dbSet;

        /// <summary>
        /// Создает новый экземпляр репозитория с заданным контекстом базы данных
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста базы данных</param>
        public BaseRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = this._dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Возвращает перечисление экземпляров <see cref="TEntity"/> из хранилища данных.
        /// Применяет фильтр, сортировку и загрузку связанных свойств,
        /// если заданы соответствующие значения параметров
        /// </summary>
        /// <param name="filter">Лямбда-выражение определяющее фильтрацию экземпляров <see cref="TEntity"/></param>
        /// <param name="orderBy">Лямбда-выражение определяющее сортировку экземпляров <see cref="TEntity"/></param>
        /// <param name="includeProperties">Список связанных свойств экземпляров <see cref="TEntity"/>, разделенный запятыми</param>
        /// <returns>Перечисление экземпляров <see cref="TEntity"/></returns>
        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            return GetFilteredQuery(filter, orderBy, includeProperties).ToList();
        }
        
        /// <summary>
        /// Возвращает страницу заданного размера с заданным номером
        /// в виде перечисления экземпляров <see cref="TEntity"/> из хранилища данных.
        /// Применяет фильтр, сортировку и загрузку связанных свойств,
        /// если заданы соответствующие значения параметров
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber">Лямбда-выражение определяющее фильтрацию экземпляров <see cref="TEntity"/></param>
        /// <param name="filter">Лямбда-выражение определяющее фильтрацию экземпляров <see cref="TEntity"/></param>
        /// <param name="orderBy">Лямбда-выражение определяющее сортировку экземпляров <see cref="TEntity"/></param>
        /// <param name="includeProperties">Список связанных свойств экземпляров <see cref="TEntity"/>, разделенный запятыми</param>
        /// <returns>Перечисление экземпляров <see cref="TEntity"/></returns>
        public IEnumerable<TEntity> GetPage(
            int pageSize,
            int pageNumber,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            var query = GetFilteredQuery(filter, orderBy, includeProperties);
            return query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }

        /// <summary>
        /// Возвращает экземпляр <see cref="TEntity"/>,
        /// соответствующий заданному идентификатору, из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Экземпляр <see cref="TEntity"/></returns>
        public TEntity GetById(int id)
        {
            return this._dbSet.Find(id);
        }

        /// <summary>
        /// Добавляет заданный экземпляр <see cref="TEntity"/> в хранилище данных
        /// </summary>
        /// <param name="entity">Экземпляр <see cref="TEntity"/></param>
        public void Insert(TEntity entity)
        {
            this._dbSet.Add(entity);
        }

        /// <summary>
        /// Обновляет заданный экземпляр <see cref="TEntity"/> в хранилище данных
        /// </summary>
        /// <param name="entity">Экземпляр <see cref="TEntity"/></param>
        public void Update(TEntity entity)
        {
            this._dbSet.Attach(entity);
            this._dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Удаляет экземпляр <see cref="TEntity"/>,
        /// соответствующий заданному идентификатору, из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public void Delete(int id)
        {
            TEntity entityToDelete = this._dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Удаляет заданный экземпляр <see cref="TEntity"/> из хранилища данных
        /// </summary>
        /// <param name="entity">Экземпляр <see cref="TEntity"/></param>
        public void Delete(TEntity entity)
        {
            if (this._dbContext.Entry(entity).State == EntityState.Detached)
            {
                this._dbSet.Attach(entity);
            }
            this._dbSet.Remove(entity);
        }

        /// <summary>
        /// Возвращает количество элементов в репозитории,
        /// соответствующих заданному фильтру
        /// </summary>
        /// <param name="filter">Лямбда-выражение определяющее фильтрацию экземпляров <see cref="TEntity"/></param>
        /// <returns>Количество элементов</returns>
        public int ItemsCount(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                return this._dbSet.Count(filter);
            }

            return this._dbSet.Count();
        }

        private IQueryable<TEntity> GetFilteredQuery(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties)
        {
            IQueryable<TEntity> query = this._dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }
    }
}