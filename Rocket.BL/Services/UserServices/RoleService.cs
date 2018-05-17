using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.DAL.Common.DbModels.DbUserRole;
using Rocket.DAL.Common.DbModels.Parser;
using Rocket.DAL.Common.UoW;
using Rocket.DAL.Context;
using Rocket.DAL.Repositories;

namespace Rocket.BL.Services.UserServices
{
    /// <summary>
    /// получение роли, установка роли для пользователя
    /// если не указана, то дефолтовая
    /// </summary>
    public class RoleService : BaseService // todo add ilogger
    {
        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool RoleIsExists(Expression<Func<Role, bool>> filter)
        {
            return _unitOfWork.RoleRepository
                .Get(Mapper.Map<Expression<Func<DbRole, bool>>>(filter))
                .Any();
        }

        public IEnumerable<Role> Get(Expression<Func<DbRole, bool>> filter = null,
            Func<IQueryable<DbRole>, IOrderedQueryable<DbRole>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.RoleRepository.Get(filter, orderBy, includeProperties).Select(Mapper.Map<Role>);
        }


        public Role GetById(int id)
        {
            return Mapper.Map<Role>(
                _unitOfWork.TVSeriesRepository.GetById(id));
        }

        public void Insert(Role role)
        {
            var dbRole = Mapper.Map<DbRole>(role);
            _unitOfWork.RoleRepository.Insert(dbRole);
            _unitOfWork.Save();
        }

        public void Update(Role role)
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