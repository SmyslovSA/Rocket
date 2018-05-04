namespace Rocket.BL.Common.Models.UserRoles
{
    public interface IUser
    {
        /// <summary>
        /// id пользователя // todo можно взять модель User, если будет
        /// </summary>
        int UserId { get; set; }
    }
}