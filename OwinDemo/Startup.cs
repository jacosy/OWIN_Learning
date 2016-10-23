using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Diagnostics;
using OwinDemo.Middlewares;

[assembly: OwinStartup(typeof(OwinDemo.Startup))]

namespace OwinDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 如需如何設定應用程式的詳細資訊，請參閱  http://go.microsoft.com/fwlink/?LinkID=316888

            // Middleware Method 1
            //app.Use(async (context, next) =>
            //{
            //    Debug.WriteLine($"Incoming Request: {context.Request.Path.Value}");
            //    await next();
            //    Debug.WriteLine($"Outgoing Response: {context.Response.ContentType}");
            //});

            // Middleware Method 2
            //app.Use<DebugMiddleware>(new DebugMiddlewareOptions() {
            //    OnIncomingRequest = context =>
            //    {
            //        var watch = new Stopwatch();
            //        watch.Start();
            //        context.Environment["DebugStopWatch"] = watch;
            //    },
            //    OnOutgoingRequest = context =>
            //    {
            //        var watch = (Stopwatch)context.Environment["DebugStopWatch"];
            //        watch.Stop();
            //        Debug.WriteLine($"Request took: {watch.ElapsedMilliseconds} ms");
            //    }
            //});

            // Middleware method 3
            app.UseDebug(new DebugMiddlewareOptions
            {
                OnIncomingRequest = context =>
               {
                   var watch = new Stopwatch();
                   watch.Start();
                   context.Environment["DebugStopWatch"] = watch;
               },
                OnOutgoingRequest = context =>
                {
                    var watch = (Stopwatch)context.Environment["DebugStopWatch"];
                    watch.Stop();
                    Debug.WriteLine($"Request took: {watch.ElapsedMilliseconds} ms");
                }
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
