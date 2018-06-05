using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Common.Logging;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Rocket.BL.Common.Models.UserRoles;
using Rocket.BL.Common.Services;
using Rocket.DAL.Common.DbModels.Identity;
using Rocket.DAL.Common.UoW;
using Rocket.DAL.Identity;

namespace Rocket.BL.Services.UserServices
{
    public class RoleService : BaseService //, IRoleService
    {
        private readonly ILog _logger;
        private readonly RockeRoleManager _roleManager;

        public RoleService(IUnitOfWork unitOfWork, ILog logger, RockeRoleManager roleManager) 
            : base(unitOfWork)
        {
            _logger = logger;
            _roleManager = roleManager;
        }

        public async void RoleIsExists(Expression<Func<Role, bool>> filter)
        {
            //return await _roleManager.RoleExistsAsync(filter).ConfigureAwait(false);

            //return _unitOfWork.RoleRepository
            //    .Get(Mapper.Map<Expression<Func<DbRole, bool>>>(filter))
            //    .Any();
        }

        public IEnumerable<Role> Get(
            Expression<Func<DbRole, bool>> filter = null, 
            Func<IQueryable<DbRole>, IOrderedQueryable<DbRole>> orderBy = null, 
            string includeProperties = "")
        {
            return _unitOfWork.RoleRepository.Get(filter, orderBy, includeProperties).Select(Mapper.Map<Role>);
        }

        public async Task<Role> GetById(string id)
        {
            return await _roleManager.FindByIdAsync();

            //return Mapper.Map<Role>(
            //    _unitOfWork.RoleRepository.GetById(id));
        }

        public async Task<IdentityResult> Insert(DbRole role)
        {
            return await _roleManager.CreateAsync(role).ConfigureAwait(false); 

            //var dbRole = Mapper.Map<DbRole>(role);
            //_unitOfWork.RoleRepository.Insert(dbRole);
            //_logger.Debug($"Role {dbRole} added in DB");
            //_unitOfWork.SaveChanges();
        }

        public async Task<IdentityResult> Update(DbRole role)
        {
            return await _roleManager.UpdateAsync(role).ConfigureAwait(false);

            //var dbRole = Mapper.Map<DbRole>(role);
            //_unitOfWork.RoleRepository.Update(dbRole);
            //_logger.Debug($"Role {dbRole} updated in DB");
            //_unitOfWork.SaveChanges();
        }

        public async Task<IdentityResult> Delete(DbRole role)
        {
            return await _roleManager.DeleteAsync(role).ConfigureAwait(false);

            //_unitOfWork.RoleRepository.Delete(id);
            //_logger.Debug($"Role {id} removed from DB");
            //_unitOfWork.SaveChanges();
        }
    }
}