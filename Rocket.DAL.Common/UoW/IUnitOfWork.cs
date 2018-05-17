using Rocket.DAL.Common.Repositories.IDbPersonalAreaRepository;
using Rocket.DAL.Common.Repositories.ReleaseList;
using Rocket.DAL.Common.Repositories.User;
using System;
using Rocket.DAL.Common.Repositories.IDbUserRoleRepository;
using Rocket.DAL.Common.Repositories.Notification;

namespace Rocket.DAL.Common.UoW
{
    /// <summary>
    /// Представляет общий интерфейс unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Возвращает репозиторий для релизов.
        /// </summary>
        IDbReleaseRepository ReleaseRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для фильмов.
        /// </summary>
        IDbFilmRepository FilmRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для сериалов.
        /// </summary>
        IDbTVSeriesRepository TVSeriesRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для музыки.
        /// </summary>
        IDbMusicRepository MusicRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для юзеров.
        /// </summary>
        IDbUserRepository UserRepository { get; }

        /// <summary>
        /// ВОзвращает репозиторий ролей.
        /// </summary>
        IDbRoleRepository RoleRepository { get; }

        /// <summary>
        /// Возвращает репозиторий пермишенов.
        /// </summary>
        IDbPermissionRepository PermissionRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для emails.
        /// </summary>
        IDbEmailRepository EmailRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для genre.
        /// </summary>
        IDbGenreRepository GenreRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для пользователей личного кабинета.
        /// </summary>
        IDbAuthorisedUserRepository UserAuthorisedRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для сообщений произвольного содержания
        /// </summary>
        IDbCustomMessageRepository CustomMessageRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для шаблонов email сообщений
        /// </summary>
        IDbEmailTemplateRepository EmailTemplateRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для сообщений с информацией
        /// о совершенном гостем донате
        /// </summary>
        IDbGuestBillingMessageRepository GuestBillingMessageRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для пользователей, являющихся получателями сообщений
        /// </summary>
        IDbReceiverRepository ReceiverRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для сообщений о релизе
        /// </summary>
        IDbReleaseMessageRepository ReleaseMessageRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для сообщений с информацией о совершенных
        /// пользователем платежах на сайте (покупка премиум аккаунта, донат)
        /// </summary>
        IDbUserBillingMessageRepository UserBillingMessageRepository { get; }

        /// <summary>
        /// Сохраняет изменения в хранилище данных.
        /// </summary>
        void Save();
    }
}