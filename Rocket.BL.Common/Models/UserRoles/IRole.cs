namespace Rocket.BL.Common.Models.UserRoles
{
    public interface IRole
    {
        /// <summary>
        /// Уникальный идентификатор роли
        /// </summary>
        ushort RoleId { get; set; }
    }
}