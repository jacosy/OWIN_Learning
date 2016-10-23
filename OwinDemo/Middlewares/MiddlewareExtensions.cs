using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OwinDemo.Middlewares;

namespace Owin
{
    public static class MiddlewareExtensions
    {
        public static IAppBuilder UseDebug(this IAppBuilder app, DebugMiddlewareOptions options = null)
        {
            if (options == null)
            {
                options = new DebugMiddlewareOptions();
            }
            app.Use<DebugMiddleware>(options);

            return app;
        }
    }
}