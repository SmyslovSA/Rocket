using System;
using System.Linq.Expressions;
using AutoMapper;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.BL.Common.Services;
using Rocket.DAL.Common.DbModels.DbUserRole;
using Rocket.DAL.Common.UoW;

namespace Rocket.BL.Services.UserServices
{
    /// <summary>
    /// получение роли, установка роли для пользователя
    /// если не указана, то дефолтовая
    /// </summary>
    public class UserRoleService : BaseService, IRoleService //todo add ilogger
    {

        public UserRoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


        public Role GerRoleById(int id)
        {
            return Mapper.Map<Role>(
                _unitOfWork.TVSeriesRepository.GetById(id));
        }

        public void AddNewRole(Role role)
        {
            var dbRole = Mapper.Map<DbRole>(role);
            _unitOfWork.RoleRepository.Insert(dbRole);
            _unitOfWork.Save();
        }

        public void UpdateRole(Role role)
        {
            var dbRole = Mapper.Map<DbRole>(role);
            _unitOfWork.RoleRepository.Update(dbRole);
            _unitOfWork.Save();
        }

        public void DeleteRole(int id)
        {
            _unitOfWork.RoleRepository.Delete(id);
            _unitOfWork.Save();
        }

        public void SwitchRoleActivity(Role role)
        {
            // todo
            // _unitOfWork.RoleRepository.SwitchActivity(role);
        }

        public bool RoleIsExists(Expression<Func<Role, bool>> filter)
        {
            // todo
            return true;
        }

        private const Role DefaultRole = null;
        public void ChangeUserRole()
        {
            // todo добавить роль по умолчанию в куда-нибудь
            ChangeUserRole(DefaultRole);
        }

        public void ChangeUserRole(Role role)
        {
            // todo
        }


    }
}
