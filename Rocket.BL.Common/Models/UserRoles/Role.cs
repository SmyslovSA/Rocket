﻿using System.Collections.Generic;

namespace Rocket.BL.Common.Models.UserRoles
{
    public class Role
    {
        /// <summary>
        /// Уникальный идентификатор роли пользователя
        /// </summary>
        public ushort RoleId { get; set; }

        /// <summary>
        /// Название роли пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Свойство позволяет управлять возможностью
        /// менять доступ к использованию роли
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// список пермишенов для роли
        /// </summary>
        public ICollection<Permission> Permissions { get; set; }

        /// <summary>
        /// список юзеров с ролью
        /// </summary>
        public ICollection<User.User> Users { get; set; }
    }
}