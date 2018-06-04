﻿using AutoMapper;
using Rocket.BL.Common.Models.UserPayment;
using Rocket.DAL.Common.DbModels;
using Rocket.DAL.Common.UoW;

namespace Rocket.BL.Services.UserPaymentService
{
    /// <summary>
    /// Представляет сервис для работы с платежами
    /// </summary>
    public class UserPaymentService : BaseService, Common.Services.UserPayment.IUserPaymentService
    {
        public UserPaymentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// получение инфы о платеже.
        /// </summary>
        /// <param name="user">Экземпляр пользователя, для которого ищем инфу о платеже.</param>
        /// <returns> foobar </returns>
        public UserPayment GetUserPayment(Common.Models.User.User user)
        {
            var dbPayment = _unitOfWork.UserPaymentRepository.Get(p => p.User.Id == user.Id);

            return Mapper.Map<UserPayment>(dbPayment);
        }

        /// <summary>
        /// добавление инфы о платеже.
        /// </summary>
        /// <param name="payment">Экземпляр пользователя, для которого ищем инфу о платеже.</param>
        public void AddUserPayment(UserPayment payment)
        {
            var dbPayment = Mapper.Map<DbUserPayment>(payment);
            _unitOfWork.UserPaymentRepository.Insert(dbPayment);
            _unitOfWork.SaveChanges();
        }
    }
}