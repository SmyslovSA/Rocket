﻿using Rocket.DAL.Common.DbModels.ReleaseList;

namespace Rocket.DAL.Common.Repositories.ReleaseList
{
    /// <summary>
    /// Представляет репозиторий для людей (актёров, режиссёров)
    /// </summary>
    public interface IDbPersonRepository : IBaseRepository<DbPerson>
    {
    }
}