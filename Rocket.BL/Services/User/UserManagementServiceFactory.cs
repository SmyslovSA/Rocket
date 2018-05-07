namespace Rocket.BL.AOP.User.Interceptors
{
    using Castle.DynamicProxy;
    using Rocket.BL.Common.Services.User;

    /// <summary>
    /// Фабрика для возвращения прокси для реализации сквозного функционала 
    /// логирования для сервиса 'UserManagementService'.
    /// Для получения экземпляра прокси нужно осуществить вызов
    /// следующей сигнатуры
    /// 'UserManagementService userManagementService = UserManagementServiceFactory.CreateService<UserManagementService, IUserManagementService>()';
    /// </summary>
    public class UserManagementServiceFactory
    {
        public static TInterface CreateService<TImplementation, TInterface>()

        where TInterface : class, IUserManagementServiceBase

        where TImplementation : TInterface, IUserManagementServiceBase, new()

        {
            var dp = new ProxyGenerator();

            TInterface target = new TImplementation();

            IInterceptor interceptor = new LogInterceptor();

            return dp.CreateInterfaceProxyWithTarget<TInterface>(target, interceptor);
        }
    }
}
