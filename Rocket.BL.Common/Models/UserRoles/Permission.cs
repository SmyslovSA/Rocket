namespace Rocket.BL.Common.Models.UserRoles
{
    public class Permission
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

        /// <summary>
        /// ���������� ����������, �� ������� ���������� ���������� ����
        /// </summary>
        public string ValueName { get; set; }
    }
}
