﻿namespace Rocket.BL.Common.Models.Notification
{
    /// <summary>
    /// Описывает гостя либо пользователя, 
    /// являющихся получателями сообщения
    /// </summary>
    public class Receiver
    {
        /// <summary>
        /// Возвращает или задает имя гостя или пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает или задает email адрес гостя или пользователя
        /// </summary>
        public string Email { get; set; }
    }
}