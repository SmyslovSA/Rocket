namespace Rocket.BL.Common.Models.Notification
{
    /// <summary>
    /// Описывает сообщение со ссылкой для завершения
    /// регистрации аккаунта пользователя
    /// </summary>
    public class ConfirmationMessage
    {
        /// <summary>
        /// Возвращает или задает получателя сообщения
        /// </summary>
        public Receiver Receiver { get; set; }

        /// <summary>
        /// Возвращает или задает ссылку для завершения регистрации
        /// </summary>
        public string Link { get; set; }
    }
}