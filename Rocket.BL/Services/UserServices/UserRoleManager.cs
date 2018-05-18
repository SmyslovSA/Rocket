using System;
using Rocket.DAL.Common.UoW;
using System.Collections.Generic;
using System.Linq;
using Rocket.DAL.Common.DbModels.DbUserRole;

namespace Rocket.BL.Services.UserServices
{
    public class UserRoleManager : BaseService, IUserRoleManager
    {
        public UserRoleManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private const int DefaultRoleId = 0; // todo закинуть в хранилище дефолтроль

        /// <summary>
        /// add user to role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public virtual void AddToRole(int userId, int roleId = DefaultRoleId)
        {
            // todo check user

            if (IsInRole(userId, roleId))
            {
                return;
            }

            var dbRole = _unitOfWork.RoleRepository.GetById(roleId);
            var dbUser = _unitOfWork.UserRepository.GetById(userId);

            dbUser.Roles.Add(dbRole);

            _unitOfWork.SaveChanges();
        }

        public virtual bool RemoveFromRole(int userId, int roleId)
        {
            // todo check user

            if (!IsInRole(userId, roleId))
            {
                return false;
            }

            var dbRole = _unitOfWork.RoleRepository.GetById(roleId);
            var dbUser = _unitOfWork.UserRepository.GetById(userId);

            dbUser.Roles.Remove(dbRole);

            _unitOfWork.SaveChanges();
            return true;
        }

        /// <summary>
        /// Returns the roles for the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual IEnumerable<DbRole> GetRoles(int userId)
        {
            var dbUser = _unitOfWork.UserRepository.Get(t => t.Id == userId, includeProperties: "Roles").First();
            return dbUser.Roles;
        }

        /// <summary>
        /// Returns true if the user is in the specified role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public virtual bool IsInRole(int userId, int roleId)
        {
            if (_unitOfWork.UserRepository.GetById(userId) == null)
            {
                throw new InvalidOperationException("user not found by userId");
            }

            var roles = GetRoles(userId);
            return roles.Contains(_unitOfWork.RoleRepository.GetById(roleId));
        }
    }
}