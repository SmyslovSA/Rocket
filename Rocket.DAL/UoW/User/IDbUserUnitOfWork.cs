using Rocket.DAL.Common.Repositories.IDbUserRepository;

namespace Rocket.DAL.Common.UoW.User
{
    /// <summary>
    /// Представляет unit of work для работы с пользователями
    /// </summary>  
    public class IDbUserUnitOfWork
    {
        /// <summary>
        /// Возвращает репозиторий для пользователей
        /// </summary>
        IDbUserRepository UserRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для уровней аккаунта пользователей
        /// </summary>
        IDbAccountLevelRepository AccountLevelRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для статусов аккаунта пользователей
        /// </summary>
        IDbAccountStatusRepositary AсcountStatusRepositary { get;  }

        /// <summary>
        /// Возвращает репозиторий для стран адресов и гражданства пользователей
        /// </summary>
        IDbCountryRepositary CountryRepositary { get; }

        /// <summary>
        /// Возвращает репозиторий для языков пользователей
        /// </summary>
        IDbLanguageRepositary LanguageRepositary { get;  }
    }
}
