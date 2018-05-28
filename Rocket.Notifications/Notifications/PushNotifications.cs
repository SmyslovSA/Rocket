using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.DAL.Common.Enums;
using Rocket.DAL.Common.UoW;
using Rocket.Notifications.Interfaces;

namespace Rocket.Notifications.Notifications
{
    public class PushNotifications : IPushNotifications
    {
        private readonly IUnitOfWork _unitOfWork;

        public PushNotifications(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Отправка уведомлений
        /// </summary>
        /// <returns></returns>
        public async Task NotifyAsync()
        {
            try
            {
                var currDateTime = DateTime.Now;

                var musicReleases = GetMusicReleases(currDateTime);

            }
            catch (Exception e)
            {
                //todo логирование
                Console.WriteLine(e);
                throw e;
            }
        }

        private IEnumerable<DbMusic> GetMusicReleases(DateTime currDateTime)
        {
            var musicReleses = _unitOfWork.MusicRepository.Queryable()
                .Where(m => m.ReleaseDate > currDateTime.AddDays(-3) && m.ReleaseDate < currDateTime);  //todo settings

            //находим релизы, которые, возможно, уже были обработаны ранее
            var notificationsLog = _unitOfWork.NotificationsLogRepository.Queryable()
                .Join(musicReleses, 
                log => log.ReleaseId,
                release => release.Id,
                (log, release) => log)
                .Where(l => l.NotificationType == NotificationType.Push 
                && l.ReleaseType == ReleaseType.Music
                && l.CreatedDateTime > currDateTime.AddDays(-3)).ToList();

            //исключить релизы, которые, возможно, уже были обработаны ранее
            musicReleses = musicReleses.Where(m => notificationsLog.All(n => m.Id != n.ReleaseId));

            return musicReleses;
        }
    }
}
