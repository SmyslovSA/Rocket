using AutoMapper;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
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
        /// <param name="model">Авторизованный пользователь.</param>
        /// <param name="email">Email, который необходимо добавить.</param>
        /// <returns>True - если email добавлени, false - если не добавлен по причинам(не валидный или такойже уже привязан к нему).</returns>
        public bool AddEmail(SimpleUser model, string email)
        {
            if (model != null && string.IsNullOrEmpty(email))
            {
                //проверка на валидный емэйл (содержит@ и все такое)
                //TODO: нааписать  метод проверки валидности email
                //выбросить сообщение о не валидности добавляемого email;
                if (string.IsNullOrEmpty(email)) 
                {
                    return false;
                }
                // вытягиваю емэйлы из таблицы если он имеется

                if (_unitOfWork.EmailRepository.Get()
                        .FirstOrDefault(c => c.Name == email) == null)
                {
                    // реализация добавления в базу
                    var user = Mapper.Map<DbAuthorisedUser>(model);
                    user.Email.Add(new DbEmail
                    {
                        Name = email
                    });
                    _unitOfWork.UserAuthorisedRepository.Update(user);
                    _unitOfWork.Save();
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool DeleteEmail(SimpleUser model, string email)
        {
            if (model != null && string.IsNullOrEmpty(email))
            {
                //проверка на валидный емэйл (содержит@ и все такое)
                //TODO: написать  метод проверки валидности email
                //выбросить сообщение о не валидности добавляемого email;
                if (string.IsNullOrEmpty(email)) 
                {
                    return false;
                }
                // вытягиваю емэйл из таблицы если он имеется

                if (_unitOfWork.EmailRepository.Get()
                        .FirstOrDefault(c => c.Name == email) != null)
                {
                    //находим email
                    var emailUserEmail = _unitOfWork.EmailRepository.Get()
                        .FirstOrDefault(c => c.Name == email);

                    //прибиваем и сохраняем
                    _unitOfWork.EmailRepository.Delete(emailUserEmail);
                    _unitOfWork.Save();
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}