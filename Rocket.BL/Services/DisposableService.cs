using Rocket.DAL.Common.UoW;
using System;

namespace Rocket.BL.Services
{
    public abstract class DisposableService : IDisposable
    {
        protected IUnitOfWork _unitOfWork;
        private bool disposedValue = false;

        /// <summary>
        /// Инициализирует поле unitOfWork заданным экземпляром
        /// </summary>
        /// <param name="unitOfWork">Экземпляр unit of work</param>
        protected DisposableService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Освобождает управляемые ресурсы
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Освобождает управляемые ресурсы
        /// </summary>
        /// <param name="disposing">Указывает был ли уже вызван метода Dispose ранее</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this._unitOfWork.Dispose();
                }

                disposedValue = true;
            }
        }
    }
}