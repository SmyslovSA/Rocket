using Rocket.DAL.Common.UoW;
using System;

namespace Rocket.BL.Services
{
    public abstract class BaseService : IDisposable
    {
        protected IUnitOfWork _unitOfWork;
        private bool _disposedValue = false;

        /// <summary>
        /// Инициализирует поле unitOfWork заданным экземпляром
        /// </summary>
        /// <param name="unitOfWork">Экземпляр unit of work</param>
        protected BaseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Освобождает управляемые ресурсы
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Освобождает управляемые ресурсы
        /// </summary>
        /// <param name="disposing">Указывает вызван ли этот метод из метода Dispose() или из финализатора</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
                    GC.SuppressFinalize(this);
                }

                this._unitOfWork?.Dispose();
                this._unitOfWork = null;
                this._disposedValue = true;
            }
        }

        ~BaseService()
        {
            this.Dispose(false);
        }
    }
}