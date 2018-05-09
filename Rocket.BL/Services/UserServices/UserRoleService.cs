using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.DAL.Common.DbModels.DbUserRole;
using Rocket.DAL.Common.Repositories.IDbUserRoleRepository;
using Rocket.DAL.Common.UoW;

namespace Rocket.BL.Services.UserServices
{
    /// <summary>
    /// получение роли, установка роли для пользователя
    /// если не указана, то дефолтовая
    /// </summary>
    public class UserRoleService : BaseService, IDbRoleRepository //todo add ilogger
    {


        public UserRoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<DbRole> Get(Expression<Func<DbRole, bool>> filter = null,  // ?2
                Func<IQueryable<DbRole>, IOrderedQueryable<DbRole>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public DbRole GetById(int id)  // ?1
        {
            return _unitOfWork.RoleRepository.GetById(id);
        }

        public int AddRole(DbRole role)
        {
            var dbRole = Mapper.Map<DbRole>(role);
            this._unitOfWork.RoleRepository.Insert(dbRole);
            this._unitOfWork.Save();
            return dbRole.Id;
        }

        public void UpdateRole(Role role)
        {
            var dbRole = Mapper.Map<DbRole>(role);
            _unitOfWork.RoleRepository.Update(dbRole);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.RoleRepository.Delete(id);
            _unitOfWork.Save();
        }

        public void Delete(DbRole entity) // лишний.
        {
            throw new NotImplementedException();
        }
    }
}
