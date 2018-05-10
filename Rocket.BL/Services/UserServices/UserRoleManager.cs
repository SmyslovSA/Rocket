using Rocket.BL.Common.Models.UserRoles;
using Rocket.BL.Common.Services;
using Rocket.DAL.Common.UoW;

namespace Rocket.BL.Services.UserServices
{
    public class UserRoleManager : BaseService, IRoleService
    {
        public UserRoleManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        private const Role DefaultRole = null; // todo закинуть в хранилище дефолтроль
        public void SetDefaultRole()
        {
            // проверяем есть ли дефолт роль
            
            // 

        }
 
        // todo переделать на один метод
        public void ChangeUserRole(Role role)
        {
            throw new System.NotImplementedException();
        }
    }
}
