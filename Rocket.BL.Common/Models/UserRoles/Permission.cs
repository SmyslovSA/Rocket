namespace Rocket.BL.Common.Models.UserRoles
{
    public class Permission
    {
        /// <summary>
        /// Уникальный идентификатор значения "право доступа" 
        /// (либо функциональная возможность)
        /// </summary>
        public ushort PermisssionId { get; set; }

        /// <summary>
        /// Описание  функц. возможности, соответствующее идентификатору 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Именование переменной, за которой скрывается реализация фичи
        /// </summary>
        public string ValueName { get; set; }
    }
}
