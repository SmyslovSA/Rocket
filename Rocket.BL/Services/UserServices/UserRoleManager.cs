using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Rocket.BL.Common.Services;
using Rocket.DAL.Common.DbModels.Identity;
using Rocket.DAL.Common.UoW;

namespace Rocket.BL.Services.UserServices
{
    public class UserRoleManager : BaseService, IUserRoleManager
    {
        private const int DefaultRoleId = 1; // todo MP закинуть в хранилище дефолтроль
        private readonly ILog _logger;

        public UserRoleManager(IUnitOfWork unitOfWork, ILog logger) : base(unitOfWork)
        {
            _logger = logger;
        }

        /// <summary>
        /// add user to role
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="roleId"> Идентификатор роли. </param>
        public virtual void AddToRole(int userId, int roleId = DefaultRoleId)
        {
            var dbUser = _unitOfWork.UserRepository.Find(userId);
            var dbUserRole = _unitOfWork.UserRoleRepository.Get(t => t.UserId == userId && t.RoleId == roleId).FirstOrDefault();
            if (dbUserRole != null)
            {
                //_logger.Trace($" Role {dbRole.Name} was in user: {dbUser.Id} -- {dbUser.FirstName}{dbUser.LastName}");
                return;
            }
            // todo MP check user

            dbUserRole = new DbUserRole { UserId = userId, RoleId = roleId };
            _unitOfWork.UserRoleRepository.Insert(dbUserRole);
            _unitOfWork.SaveChanges();

            //_logger.Fatal(
            //    $"Role {dbRole.Name} was not added to user: {dbUser.Id} -- {dbUser.FirstName}{dbUser.LastName}",
            //    new Exception(string.Join(Environment.NewLine, result.Errors)));
        }

        /// <summary>
        /// Удалить роль у юзера
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="roleId"> Идентификатор роли. </param>
        /// <returns> bool </returns>
        public virtual bool RemoveFromRole(int userId, int roleId)
        {
            // todo MP check user

            if (!IsInRole(userId, roleId))
            {
                return false;
            }

            var dbUser = _unitOfWork.UserRepository.Find(userId);
            var dbUserRole = _unitOfWork.UserRoleRepository.Get(t => t.UserId == userId && t.RoleId == roleId).FirstOrDefault();
            _unitOfWork.UserRoleRepository.Delete(dbUserRole);

            //dbUser.Roles.Remove(dbRole);
            //_logger.Trace($"Role {dbRole.Name} removed from user: {dbUser.Id} -- {dbUser.FirstName}{dbUser.LastName} ");

            _unitOfWork.SaveChanges();
            return true;
        }

        /// <summary>
        /// Returns the roles for the user
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <returns>Список ролей</returns>
        public virtual IEnumerable<DbRole> GetRoles(int userId)
        {
            var dbUser = _unitOfWork.UserRepository.Get(t => t.Id == userId, includeProperties: "Roles").First();
            _logger.Trace($"Checking roles for user: {dbUser.Id} -- {dbUser.FirstName}{dbUser.LastName} ");
            return dbUser.Roles.Select(t => t.Role);
        }

        /// <summary>
        /// Returns true if the user is in the specified role
        /// </summary>
        /// <param name="userId"> Идентификатор пользователя. </param>
        /// <param name="roleId"> Идентификатор роли. </param>
        /// <returns>bool</returns>
        public virtual bool IsInRole(int userId, int roleId)
        {
            if (_unitOfWork.UserRepository.GetById(userId) == null)
            {
                throw new InvalidOperationException("user not found by userId");
            }

            var roles = GetRoles(userId);
            var res = roles.Contains(_unitOfWork.RoleRepository.GetById(roleId));

            _logger.Trace($"Checking user {userId} has role {roleId}? - {res} ");
            return res;
        }
    }
}