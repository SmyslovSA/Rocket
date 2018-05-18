using Common.Logging.Configuration;
using Common.Logging.NLog;
using NLog.Targets;

namespace Rocket.Web
{
    public class LoggerConfig
    {
        public static void Configure()
        {
            var config = NLog.LogManager.Configuration;
            var fileTarget = new FileTarget(){};

            config.AddTarget(fileTarget);
            config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, fileTarget);

            var prop = new NameValueCollection();
            Common.Logging.LogManager.Adapter = new NLogLoggerFactoryAdapter(prop);
        }
    }
}

