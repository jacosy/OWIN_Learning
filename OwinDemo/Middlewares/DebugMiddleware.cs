using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using AppFunc = System.Func<
    System.Collections.Generic.IDictionary<string, object>,
    System.Threading.Tasks.Task
>;
using System.Diagnostics;
using Microsoft.Owin;

namespace OwinDemo.Middlewares
{
    public class DebugMiddleware
    {
        private AppFunc _next;
        private DebugMiddlewareOptions _options;

        public DebugMiddleware(AppFunc next, DebugMiddlewareOptions options)
        {
            _next = next;
            _options = options;

            if(_options.OnIncomingRequest == null)
            {
                _options.OnIncomingRequest = (context) => Debug.WriteLine($"Incoming Request: {context.Request.Path.Value}");                
            }

            if (_options.OnOutgoingRequest == null)
            {
                _options.OnOutgoingRequest = (context) => Debug.WriteLine($"Outgoing Response: {context.Response.ContentType}");
            }
        }

        public async Task Invoke(IDictionary<string, object> enviroment)
        {
            var context = new OwinContext();

            _options.OnIncomingRequest(context);           
            await _next(enviroment);
            _options.OnOutgoingRequest(context);            
        }
    }

    public class DebugMiddlewareOptions
    {
        public Action<IOwinContext> OnIncomingRequest { get; set; }
        public Action<IOwinContext> OnOutgoingRequest { get; set; }
    }
}