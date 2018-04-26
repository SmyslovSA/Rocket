using Rocket.DAL.Common.Repositories;

namespace Rocket.DAL.Common.UoW
{
	/// <summary>
	/// Представляет unit of work для работы с музыкальными релизами
	/// </summary>
	interface IDbMusicUnitOfWork : IUnitOfWork
	{
		/// <summary>
		/// Возвращает репозиторий для музыкальных релизов
		/// </summary>
		IDbMusicRepository MusicRepository { get; }

		/// <summary>
		/// Возвращает репозиторий для иполнителей
		/// </summary>
		IDbMusicianRepository MusicianRepository { get; }

		/// <summary>
		/// Возвращает репозиторий для музыкальных жанров
		/// </summary>
		IDbMusicGenreRepository MusicGenreRepository { get; }

		/// <summary>
		/// Возвращает репозиторий для музыкальных треков
		/// </summary>
		IDbMusicTrackRepository MusicTrackRepository { get; }
	}
}
