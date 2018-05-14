using Rocket.DAL.Common.DbModels.ReleaseList;

namespace Rocket.DAL.Common.DbModels.Notification
{
    public class DbPerformer
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор музыкального исполнителя
        /// из модели <see cref="DbMusician"/>
        /// </summary>
        public int MusicianId { get; set; }

        /// <summary>
        /// Возвращает или задает идентификационный номер сообщения
        /// о музыкальном релизе, к которому принадлежит исполнитель
        /// </summary>
        public int MusicMessageId { get; set; }

        /// <summary>
        /// Возвращает или задает сообщение о музыкальном релизе,
        /// к которому принадлежит исполнитель
        /// </summary>
        public DbMusicMessage MusicMessage { get; set; }

        /// <summary>
        /// Возвращает или задает полное имя музыкального исполнителя (название группы)
        /// </summary>
        public string Name { get; set; }
    }
}