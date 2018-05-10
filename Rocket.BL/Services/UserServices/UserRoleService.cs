using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.DAL.Common.DbModels.DbUserRole;
using Rocket.DAL.Common.UoW;

namespace Rocket.BL.Services.UserServices
{
    /// <summary>
    /// получение роли, установка роли для пользователя
    /// если не указана, то дефолтовая
    /// </summary>
    public class UserRoleService : BaseService // todo add ilogger
    {

        public UserRoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public bool RoleIsExists(Expression<Func<Role, bool>> filter)
        {
            return _unitOfWork.RoleRepository
                       .Get(Mapper.Map<Expression<Func<DbRole, bool>>>(filter))
                       .Any();
        }
        
        public IEnumerable<DbRole> Get(Expression<Func<DbRole, bool>> filter = null, Func<IQueryable<DbRole>, IOrderedQueryable<DbRole>> orderBy = null, string includeProperties = "")
        {
           return _unitOfWork.RoleRepository.Get(filter, orderBy, includeProperties);
        }

        
        public DbRole GetById(int id)
        {
            return Mapper.Map<DbRole>(
                _unitOfWork.TVSeriesRepository.GetById(id));
        }

        public void Insert(DbRole role)
        {
            var dbRole = Mapper.Map<DbRole>(role);
            _unitOfWork.RoleRepository.Insert(dbRole);
            _unitOfWork.Save();
        }

        public void Update(DbRole role)
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
    }
}
