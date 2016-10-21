using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Diagnostics;

[assembly: OwinStartup(typeof(OwinDemo.Startup))]

namespace OwinDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 如需如何設定應用程式的詳細資訊，請參閱  http://go.microsoft.com/fwlink/?LinkID=316888
            app.Use(async (context, next) =>
            {
                Debug.WriteLine($"Incoming Request: {context.Request.Path.Value}");
                await next();
                Debug.WriteLine($"Outcoming Response: {context.Response.ContentType}");
            });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
