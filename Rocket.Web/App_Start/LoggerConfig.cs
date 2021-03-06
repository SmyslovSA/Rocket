﻿using Common.Logging;
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
            var fileTarget = new FileTarget("fileTarget");
            var mailTarget = new MailTarget("mailTarget");

            config.AddTarget(fileTarget);
            config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Info, fileTarget);
            config.AddTarget(mailTarget);
            config.AddRule(NLog.LogLevel.Warn, NLog.LogLevel.Fatal, mailTarget);

            var prop = new NameValueCollection
            {
                { "configType", "FILE" },
                { "configFile", "~/NLog.config" }
            };
            LogManager.Adapter = new NLogLoggerFactoryAdapter(prop);
        }
    }
}