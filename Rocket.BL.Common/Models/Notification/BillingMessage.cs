namespace Rocket.BL.Common.Models.Notification
{
    /// <summary>
    /// Описывает письмо с данными о совершенных
    /// пользователем либо гостем платежах на сайте
    /// (покупка премиум аккаунта, донат)
    /// </summary>
    public class BillingMessage
    {
        /// <summary>
        /// Задает или возвращает получателя сообщения
        /// </summary>
        public Receiver Receiver { get; set; }

        /// <summary>
        /// Задает или возвращает оплаченную сумму
        /// </summary>
        public decimal Sum { get; set; }
    }
}