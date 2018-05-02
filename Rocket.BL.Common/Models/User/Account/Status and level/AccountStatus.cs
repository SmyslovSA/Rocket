﻿using System;

namespace Rocket.BL.Common.Models.User
{
    /// <summary>
    /// Представляет информацию о статусе аккаунта пользователя
    /// (активирован, не активирован, забанен, деактивирован)
    /// </summary>
    public class AccountStatus
    {
        /// <summary>
        /// Возвращает или задает уникальный идентификатор статуса аккаунта пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Возвращает или задает название статуса аккаунта
        /// (активирован, не активирован, деактивирован, забанен)
        /// </summary>
        public string Name { get; set; }
    }
}
