namespace Rocket.BL.Common.Models.UserRoles
{
    public class RolePermission
    {
        /// <summary>
        /// ���������� ������������� �������� "����� �������" 
        /// (���� �������������� �����������)
        /// </summary>
        public ushort PermisssionId { get; set; }

        /// <summary>
        /// ��������  �����. �����������, ��������������� �������������� 
        /// </summary>
        public string Description { get; set; }
    }
}
