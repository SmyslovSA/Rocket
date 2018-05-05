using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using CommonServiceLocator;
using Rocket.DAL.Common.Repositories;
using Rocket.DAL.Common.Repositories.Temp;
using Rocket.DAL.Common.UoW;
using Rocket.DAL.Context;
using Rocket.DAL.Repositories;

namespace Rocket.DAL.UoW
{
    public class UnitOfWork: IUnitOfWork
    {
        private RocketContext _rocketContext;
        private bool _disposed;
        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(RocketContext rocketContext)
        {
            _rocketContext = rocketContext;
            _repositories = new Dictionary<string, dynamic>();
        }

        public IDbFilmRepository FilmRepository => throw new NotImplementedException();

        public IDbTVSeriesRepository TVSeriesRepository => throw new NotImplementedException();

        public IDbMusicRepository MusicRepository => throw new NotImplementedException();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_rocketContext != null)
                {
                    _rocketContext.Dispose();
                    _rocketContext = null;
                }
            }
            
            _disposed = true;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return _rocketContext.SaveChanges();
        }
    }
}
