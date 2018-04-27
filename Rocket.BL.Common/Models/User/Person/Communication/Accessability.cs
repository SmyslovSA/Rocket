namespace Rocket.BL.Common.Models.User
{
    public enum Accessability
    {
        AccessPermitted = 1, // Доступ разрешен и возможен.

        AccessDenied = 2, // Доступ возможен, но запрещен.

        AccessForbidden = 3 // Доступ заблокирован.
    }
}
