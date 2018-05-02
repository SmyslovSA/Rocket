namespace Rocket.BL.Common.Models.UserRoles
{
    public class Permission : IPermission
    {
        /// <summary>
        /// uniq identificator of permission
        /// </summary>
        public ushort PermissionId { get; set; }

        /// <summary>
        /// description of permission
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// name of permission 
        /// </summary>
        public string ValueName { get; set; }
    }
}
