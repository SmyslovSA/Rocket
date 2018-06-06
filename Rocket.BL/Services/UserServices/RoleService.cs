using System.Linq;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

        public async Task<bool> RoleIsExists(string filter)
        {
            _logger.Trace($"Request RoleIsExists : filter {filter}");
            return await _roleManager.RoleExistsAsync(filter).ConfigureAwait(false);

            //return _unitOfWork.RoleRepository
            //    .Get(Mapper.Map<Expression<Func<DbRole, bool>>>(filter))
            //    .Any();
        }

        public IQueryable<IdentityRole> GetAllRoles()
        {
            _logger.Trace($"Request GetAllRoles");
            return _roleManager.Roles;
        }

        //public IEnumerable<Role> Get(
        //    Expression<Func<DbRole, bool>> filter = null, 
        //    Func<IQueryable<DbRole>, IOrderedQueryable<DbRole>> orderBy = null, 
        //    string includeProperties = "")
        //{
        //    return _unitOfWork.RoleRepository.Get(filter, orderBy, includeProperties).Select(Mapper.Map<Role>);
        //}

        public async Task<IdentityRole> GetById(string roleId)
        {
            _logger.Trace($"Request GetById : roleId {roleId}");
            return await _roleManager.FindByIdAsync(roleId).ConfigureAwait(false);

            //return Mapper.Map<Role>(
            //    _unitOfWork.RoleRepository.GetById(id));
        }

        public async Task<IdentityResult> Insert(DbRole role)
        {
            _logger.Trace($"Request Insert in queue: Role {role}");
            var result = await _roleManager.CreateAsync(role).ConfigureAwait(false);

            _logger.Trace($"Request Insert complete: Role {role}");
            return result;

            //var dbRole = Mapper.Map<DbRole>(role);
            //_unitOfWork.RoleRepository.Insert(dbRole);
            //_logger.Debug($"Role {dbRole} added in DB");
            //_unitOfWork.SaveChanges();
        }

        public async Task<IdentityResult> Update(DbRole role)
        {
            _logger.Trace($"Request Update in queue: Role {role}");
            var result = await _roleManager.UpdateAsync(role).ConfigureAwait(false);

            _logger.Trace($"Request Update complete: Role {role}");
            return result;

            //var dbRole = Mapper.Map<DbRole>(role);
            //_unitOfWork.RoleRepository.Update(dbRole);
            //_logger.Debug($"Role {dbRole} updated in DB");
            //_unitOfWork.SaveChanges();
        }

        public async Task<IdentityResult> Delete(DbRole role)
        {
            _logger.Trace($"Request Delete in queue: Role {role}");
            var result = await _roleManager.DeleteAsync(role).ConfigureAwait(false);

            _logger.Trace($"Request Delete complete: Role {role}");
            return result;

            //_unitOfWork.RoleRepository.Delete(id);
            //_logger.Debug($"Role {id} removed from DB");
            //_unitOfWork.SaveChanges();
        }
    }
}