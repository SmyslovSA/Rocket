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

        /// <summary>
        /// Добавляет пермишен роли
        /// </summary>
        /// <param name="idRole">Идентификатор роли</param>
        /// <param name="idPermission">Идентификатор пермишена</param>
        public void AddPermissionToRole(int idRole, int idPermission)
        {
            // докидываем пермишен в роль
            var role =  Mapper.Map<DbRole>(_unitOfWork.RoleRepository.GetById(idRole));
            var permission = Mapper.Map<DbPermission>(_unitOfWork.PermissionRepository.GetById(idPermission));
            role.Permissions.Add(permission);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Удоляет пермишен у роли
        /// </summary>
        /// <param name="idRole">Идентификатор роли</param>
        /// <param name="idPermission">Идентификатор пермишена</param>
        public void RemovePermissionFromRole(int idRole, int idPermission)
        {
            // удаляем пермишен у роли
            var role = Mapper.Map<DbRole>(_unitOfWork.RoleRepository.GetById(idRole));
            var permission = Mapper.Map<DbPermission>(_unitOfWork.PermissionRepository.GetById(idPermission));
            role.Permissions.Remove(permission);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Добавляет пермишен
        /// </summary>
        /// <param name="permission">Добавляет пермишен</param>
        public void Insert(Permission permission)
        {
            var dbPermission = Mapper.Map<DbPermission>(permission);
            _unitOfWork.PermissionRepository.Insert(dbPermission);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Обновляет пермишен
        /// </summary>
        /// <param name="permission">Пермишен</param>
        public void Update(Permission permission)
        {
            var dbPermission = Mapper.Map<DbPermission>(permission);
            _unitOfWork.PermissionRepository.Update(dbPermission);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Удаляет пермишен
        /// </summary>
        /// <param name="id">Идентификатор пермишена</param>
        public void Delete(int id)
        {
            _unitOfWork.PermissionRepository.Delete(id);
            _unitOfWork.Save();
        }

        /// <summary>
        /// Возвращает пермешен с заданным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        public Permission GetById(int id)
        {
            return Mapper.Map<Permission>(_unitOfWork.PermissionRepository.GetById(id));
        }

        /*
        public void IsExistPermissionInUser(DAL.Common.DbModels.User.DbUser User, int idPermission)
        {
            // докидываем пермишен в роль
            var role = Mapper.Map<DbRole>(_unitOfWork.RoleRepository.GetById(idRole));
            var permission = Mapper.Map<DbPermission>(_unitOfWork.PermissionRepository.GetById(idPermission));
            role.Permissions.Add(permission);
            _unitOfWork.Save();
        }
        */
    }
}
