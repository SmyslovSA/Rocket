using AutoMapper;
using Rocket.DAL.Common.DbModels.DbUserRole;
using Rocket.DAL.Common.UoW;
using System.Collections.Generic;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.BL.Common.Services;

namespace Rocket.BL.Services.UserServices
{
    /// <summary>
    /// Добавление/удаление пермишенов у ролей + логирование
    /// </summary>
    public class PermissionManagerService : BaseService, IPermissionService
    {
        /// <summary>
        /// Создает новый экземпляр <see cref="PermissionManagerService"/>
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
            var dbRole = _unitOfWork.RoleRepository.GetById(idRole);
            var dbPermission = _unitOfWork.PermissionRepository.GetById(idPermission);
            dbRole.Permissions.Add(dbPermission);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Удоляет пермишен у роли
        /// </summary>
        /// <param name="idRole">Идентификатор роли</param>
        /// <param name="idPermission">Идентификатор пермишена</param>
        public void RemovePermissionFromRole(int idRole, int idPermission)
        {
            // удаляем пермишен у роли
            var dbRole = _unitOfWork.RoleRepository.GetById(idRole);
            var dbPermission = _unitOfWork.PermissionRepository.GetById(idPermission);
            dbRole.Permissions.Remove(dbPermission);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Добавляет пермишен
        /// </summary>
        /// <param name="permission">Пермишен</param>
        public void Insert(Permission permission)
        {
            var dbPermission = Mapper.Map<DbPermission>(permission);
            _unitOfWork.PermissionRepository.Insert(dbPermission);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Обновляет пермишен
        /// </summary>
        /// <param name="permission">Пермишен</param>
        public void Update(Permission permission)
        {
            var dbPermission = Mapper.Map<DbPermission>(permission);
            _unitOfWork.PermissionRepository.Update(dbPermission);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Удаляет пермишен
        /// </summary>
        /// <param name="id">Идентификатор пермишена</param>
        public void Delete(int id)
        {
            _unitOfWork.PermissionRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Возвращает пермешен с заданным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Permission</returns>
        public Permission GetById(int id)
        {
            return Mapper.Map<Permission>(_unitOfWork.PermissionRepository.GetById(id));
        }

        /// <summary>
        /// Возвращает пермишены роли, нужно для UI
        /// </summary>
        /// <param name="idRole">Идентификатор роли</param>
        /// <returns>Коллекцию Permission</returns>
        public IEnumerable<Permission> GetPermissionByRole(int idRole)
        {
            var rol = _unitOfWork.RoleRepository.GetById(idRole);
            if (rol != null)
                return Mapper.Map<Role>(rol).Permissions;
            else return null;
        }

        /*
        public void IsExistPermissionInUser(DAL.Common.DbModels.User.DbUser User, int idPermission)
        {
            // докидываем пермишен в роль
            var role = Mapper.Map<DbRole>(_unitOfWork.RoleRepository.GetById(idRole));
            var permission = Mapper.Map<DbPermission>(_unitOfWork.PermissionRepository.GetById(idPermission));
            role.Permissions.Add(permission);
            _unitOfWork.SaveChanges();
        }
        */
    }
}