using System.Security;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.BL.Common.Services;
using Rocket.BL.Services.ReleaseList;
using Rocket.DAL.Common.DbModels.DbUserRole;
using Rocket.DAL.Common.UoW;
using AutoMapper;

namespace Rocket.BL.Services.UserServices
{
    /// <summary>
    /// Добавление/удаление пермишенов у ролей + логирование
    /// </summary>
    public class PermissionManagerService : BaseService
    {

        /// <summary>
        /// Создает новый экземпляр <see cref="FilmDetailedInfoService"/>
        /// с заданным unit of work
        /// </summary>
        /// <param name="unitOfWork">Экземпляр unit of work</param>
        public PermissionManagerService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void AddPermissionToRole(int idRole, int idPermission)
        {
            // докидываем пермишен в роль
            DbRole aRole =  Mapper.Map<DbRole>(_unitOfWork.RoleRepository.GetById(idRole));
            DbPermission aPermission = Mapper.Map<DbPermission>(_unitOfWork.RoleRepository.GetById(idRole));
            aRole.Permissions.Add(aPermission);
        }

        public void RemovePermissionFromRole(int idRole, int idPermission)
        {
            // удаляем пермишен у роли
            DbRole aRole = Mapper.Map<DbRole>(_unitOfWork.RoleRepository.GetById(idRole));
            DbPermission aPermission = Mapper.Map<DbPermission>(_unitOfWork.RoleRepository.GetById(idRole));
            aRole.Permissions.Remove(aPermission);
        }
    }
}
