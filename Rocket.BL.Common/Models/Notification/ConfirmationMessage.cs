namespace Rocket.BL.Common.Models.Notification
{
    /// <summary>
    /// Описывает письмо со ссылкой для завершения
    /// регистрации аккаунта пользователя
    /// </summary>
    public class ConfirmationMessage
    {
        /// <summary>
        /// Задает или возвращает получателя сообщения
        /// </summary>
        public Receiver Receiver { get; set; }

        /// <summary>
        /// Задает или возвращает ссылку для завершения регистрации
        /// </summary>
        public string Link { get; set; }
    }
}