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



        public IEnumerable<DbRole> Get(Expression<Func<DbRole, bool>> filter = null, 
                Func<IQueryable<DbRole>, IOrderedQueryable<DbRole>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public DbRole GetRoleById(int id)
        {
            return Mapper.Map<Role>(
                _unitOfWork.RoleRepository.GetById(id));
        }

        public void Insert(DbRole entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DbRole entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(DbRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
