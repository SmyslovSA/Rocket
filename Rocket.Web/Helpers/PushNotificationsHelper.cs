using System.Collections.Generic;
using System.Linq;
using Rocket.BL.Common.Models.ReleaseList;
using Rocket.BL.Common.Models.Subscription;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.DbModels.ReleaseList;
using Rocket.Web.Hubs;
using Rocket.Web.Models;

namespace Rocket.Web.Helpers
{
    /// <summary>
    /// Выполняет рассылку push-уведомлений
    /// </summary>
    public class PushNotificationsHelper : IPushNotificationsHelper
    {
        /// <summary>
        /// Отослать сообщение всем активным пользователям
        /// </summary>
        /// <param name="msg">Сообщение</param>
        public void SendPushNotificationsToAll(object msg)
        {
            NotificationHub.NotifyAll(msg);
        }

        /// <inheritdoc />
        /// <summary>
        /// Отослать сообщения о выходе релизов
        /// </summary>
        /// <param name="notifications">Коллекция push-уведомлений</param>
        public void SendPushNotificationsOfRelease(IEnumerable<PushNotificationModel> notifications) //todo presentation model
        {
            foreach (var notification in notifications)
            {
                NotificationHub.NotifyOfRelease(notification.Message, notification.Users);
            }
        }

        /// <summary>
        /// Отправляет уведомления о музыкальных релизах
        /// </summary>
        /// <param name="musicReleases"></param>
        //public void SendPushNotificationsOfRelease(IEnumerable<Music> notifications)
        //{
        //    AddSubscribabersByMusicReleases(musicReleases);
        //    foreach (var musicRelease in musicReleases)
        //    {
        //        foreach (var user in musicRelease.Users)
        //        {
        //            NotificationHub.NotifyOfRelease($"{musicRelease.Title} ", //{musicRelease.Artist}
        //                user.Id.ToString());  //todo identity id
        //        }
        //    }
        //}

        /// <summary>
        /// Добавляет подписчиков на музыкальный релиз, если те подписаны на исполнителя из этого релиза
        /// </summary>
        /// <param name="musicReleases"></param>
        //private static void AddSubscribabersByMusicReleases(IEnumerable<Music> musicReleases)
        //{
        //    foreach (var musicRelease in musicReleases)
        //    {
        //        foreach (var musician in musicRelease.Musicians)
        //        {
        //            var users = musician.Users.Except(musicRelease.Users);
        //            foreach (var user in users)
        //            {
        //                musicRelease.Users.Add(user);
        //            }
        //        }
        //    }
        //}
    }
}