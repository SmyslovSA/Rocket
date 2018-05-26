using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Common.Logging;
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
    public class RoleService : BaseService, IRoleService
    {
        private readonly ILog _logger;

        public RoleService(IUnitOfWork unitOfWork, ILog _logger) : base(unitOfWork)
        {
            this._logger = _logger;
        }

        public bool RoleIsExists(Expression<Func<Role, bool>> filter)
        {
            return _unitOfWork.RoleRepository
                .Get(Mapper.Map<Expression<Func<DbRole, bool>>>(filter))
                .Any();
        }

        public IEnumerable<Role> Get(
            Expression<Func<DbRole, bool>> filter = null, 
            Func<IQueryable<DbRole>, IOrderedQueryable<DbRole>> orderBy = null, 
            string includeProperties = "")
        {
            return _unitOfWork.RoleRepository.Get(filter, orderBy, includeProperties).Select(Mapper.Map<Role>);
        }

        public Role GetById(int id)
        {
            return Mapper.Map<Role>(
                _unitOfWork.RoleRepository.GetById(id));
        }

        public void Insert(Role role)
        {
            var dbRole = Mapper.Map<DbRole>(role);
            _unitOfWork.RoleRepository.Insert(dbRole);
            _logger.Debug($"Role {dbRole} added in DB");
            _unitOfWork.SaveChanges();
        }

        public void Update(Role role)
        {
            var dbRole = Mapper.Map<DbRole>(role);
            _unitOfWork.RoleRepository.Update(dbRole);
            _logger.Debug($"Role {dbRole} updated in DB");
            _unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            _unitOfWork.RoleRepository.Delete(id);
            _logger.Debug($"Role {id} removed from DB");
            _unitOfWork.SaveChanges();
        }
    }
}