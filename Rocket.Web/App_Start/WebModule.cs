﻿using Common.Logging;

namespace Rocket.Web
{
    public class WebModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<ILog>().ToMethod(ctx =>
            {
                ILog result;

                if (ctx.Request.ParentContext != null)
                {
                    var type = ctx.Request.ParentContext.Request.Service;
                    result = LogManager.GetLogger(type);
                }
                else
                {
                    result = LogManager.GetLogger("NLogger");
                }

                return result;
            });
        }
    }
}