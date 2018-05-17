﻿using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.Repositories.ReleaseList;
using Rocket.DAL.Context;

namespace Rocket.DAL.Repositories.ReleaseList
{
    /// <summary>
    /// Представляет репозиторий для музыкальных релизов
    /// </summary>
    public class DbMusicRepository : BaseRepository<DbMusic>, IDbMusicRepository
    {
        /// <summary>
        /// Создает новый экземпляр репозитория для музыкальных релизов
        /// с заданным контекстом базы данных
        /// </summary>
        /// <param name="dbContext">Экземпляр контекста базы данных</param>
        public DbMusicRepository(RocketContext dbContext)
            : base(dbContext)
        {
        }
    }
}