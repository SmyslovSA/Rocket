using FluentValidation;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.BL.Properties;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.UoW;
using System.Linq;

namespace Rocket.BL.Services.PersonalArea
{
    public class ChangeEmailManagerService : BaseService, IEmailManager
    {
        public ChangeEmailManagerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Метод для добавления email.
        /// </summary>
        /// <param name="id">Id авторизованного пользователь, инициировавшего смену</param>
        /// <param name="email">Email, который необходимо добавить.</param>
        public void AddEmail(int id, Email email)
        {
            if (_unitOfWork.EmailRepository.Get()
                    .FirstOrDefault(c => c.Name == email.Name) != null)
            {
                throw new ValidationException(Resources.EmailDuplicate);
            }

            var emails = new DbEmail() { Name = email.Name, DbAuthorisedUserId = id };
            _unitOfWork.EmailRepository.Insert(emails);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Метод для удаления email.
        /// </summary>
        /// <param name="id">Id email, который необходимо удалить</param>
        public void DeleteEmail(int id)
        {
            var model = _unitOfWork.EmailRepository.GetById(id);
            if (model == null)
            {
                throw new ValidationException(Resources.UndefinedEmail);
            }

            _unitOfWork.EmailRepository.Delete(model);
            _unitOfWork.SaveChanges();
        }
    }
}