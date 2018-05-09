using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class UserRoleService : BaseService, IDbRoleRepository
    {


        public UserRoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Role RequestUserRole(IUser user)
        {
            // todo возвращаем роль юзера
            // return _user?.GetUserRole();

            // todo hello crunch
            return new Role();
        }

        // todo добавить роль по умолчанию в куда-нибудь
        private const IRole DefaultRole = null;

        public void ChangeUserRole(IUser user)
        {
            ChangeUserRole(user, DefaultRole);
        }

        public void ChangeUserRole(IUser user, IRole role)
        {
            // todo сетапим роль нашему юзверю
            // _user?.SetUserRole(_role);
        }



        public IEnumerable<DbRole> Get(Expression<Func<DbRole, bool>> filter = null, 
                Func<IQueryable<DbRole>, IOrderedQueryable<DbRole>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public DbRole GetRoleById(int id)
        {
            return Mapper.Map<Role>(
                this._unitOfWork.RoleRepository.GetById(id));
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
