namespace Rocket.BL.Common.Models.Notification
{
    /// <summary>
    /// Описывает сообщение со ссылкой для завершения
    /// регистрации аккаунта пользователя
    /// </summary>
    public class ConfirmationMessage
    {
        /// <summary>
        /// Возвращает или задает имя получателя сообщения
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// Возвращает или задает email адрес получателя сообщения
        /// </summary>
        public string ReceiverEmail { get; set; }

        /// <summary>
        /// Возвращает или задает ссылку для завершения регистрации
        /// </summary>
        public string Link { get; set; }
    }
}