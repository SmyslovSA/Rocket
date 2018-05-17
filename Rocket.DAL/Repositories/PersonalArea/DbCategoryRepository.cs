﻿using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
using System.Data.Entity;

namespace Rocket.DAL.Repositories.PersonalArea
{
    /// <summary>
    /// Репозиторий категорий.
    /// </summary>
    public class DbCategoryRepository : BaseRepository<DbCategory>, IDbCategoryRepository
    {
        /// <summary>
        /// Создает новый экземпляр репозитория для категорий с заданным контекстом базы данных.
        /// </summary>
        /// <param name="context">Экземпляр контекста базы данных.</param>
        public DbCategoryRepository(DbContext context) : base(context)
        {
        }
    }
}