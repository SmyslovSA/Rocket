using NLog;
using Castle.DynamicProxy;

namespace Rocket.BL.AOP.User.Interceptors
{
    /// <summary>
    /// Перехватчик, который выполняется при всех запусках методов
    /// сервиса 'UserManagementService'
    /// </summary>
    public class LogInterceptor : IInterceptor
    {
        #region IInterceptor Members

        public void Intercept(IInvocation invocation)

        {
            var logger = LogManager.GetCurrentClassLogger();

            logger.Info($"Method {invocation.Method.Name}");

            foreach (var item in invocation.Arguments)
            {
                logger.Info($"type: {item.GetType().ToString()}, value: {item.ToString()}");
            }

            invocation.Proceed();

            logger.Info($"Post process {invocation.Method.Name} method");
        
        }

        #endregion
    }
}
