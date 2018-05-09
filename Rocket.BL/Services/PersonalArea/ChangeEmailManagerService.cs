using AutoMapper;
using Rocket.BL.Common.Models.PersonalArea;
using Rocket.BL.Common.Services.PersonalArea;
using Rocket.DAL.Common.DbModels.DbPersonalArea;
using Rocket.DAL.Common.UoW;
using System.Linq;


namespace Rocket.BL.Services.PersonalArea
{
    class ChangeEmailManagerService : BaseService, IEmailManager
    {
        public ChangeEmailManagerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        /// <summary>
        /// метод для добавления imail
        /// </summary>
        /// <param name="model">авторизованный пользователь</param>
        /// <param name="email">сам email</param>
        /// <returns>true - если email добавлени, false - если не добавлен по причинам(не валидный или такойже уже привязан к нему)</returns>
        public bool AddEmail(SimpleUser model, string email)
        {        
            if (model!=null && string.IsNullOrEmpty(email))
            {
                //проверка на валидный имейл (содержит@ и все такое)
                //TODO: нааписать  метод проверки валидности email
                if (string.IsNullOrEmpty(email)) //выбрасить сообщение о не валидности добавляемого email; )
                {
                    return false;
                }
                // вытигиваю имэйлы из таблицы если он имеется
                else if (_unitOfWork.EmailRepository.Get()
                    .FirstOrDefault(c => c.Name == email)==null)
                {
                    // реализация добавления в базу
                    var user = Mapper.Map<DbAuthorisedUser>(model);
                    user.Email.Add(new DbEmail
                    {
                        Name = email
                    });                   
                    _unitOfWork.UserRepository.Update(user);
                    _unitOfWork.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }         
            return false; 
        }

        public bool DeleteEmail(SimpleUser model, string email)
        {
            if (model != null && string.IsNullOrEmpty(email))
            {
                //проверка на валидный имейл (содержит@ и все такое)
                //TODO: нааписать  метод проверки валидности email
                if (string.IsNullOrEmpty(email)) //выбрасить сообщение о не валидности добавляемого email; )
                {
                    return false;
                }
                // вытигиваю имэйлы из таблицы если он имеется
                else if (_unitOfWork.EmailRepository.Get()
                             .FirstOrDefault(c => c.Name == email) != null)
                {
                    //находим email
                    var emailUserEmail = _unitOfWork.EmailRepository.Get()
                        .FirstOrDefault(c => c.Name==email);

                    //прибиваем и сохраняем
                    _unitOfWork.UserRepository.Delete(emailUserEmail);
                    _unitOfWork.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
