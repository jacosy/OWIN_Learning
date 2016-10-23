using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Owin;
using Nancy.Security;

namespace OwinDemo.Modules
{
    public class NancyDemoModule : NancyModule
    {
        public NancyDemoModule()
        {
            Get["/nancy"] = x =>
            {
                this.RequiresMSOwinAuthentication();

                var env = Context.GetOwinEnvironment();
                var user = Context.GetMSOwinUser();
                var currentUser = Context.CurrentUser;
                return $"Hello from Nancy! Your requested: {env["owin.RequestPath"]}\n owin.RequestPathBase: {env["owin.RequestPathBase"]}<br><br>{nameof(Context.CurrentUser)} : {currentUser}<br><br>Context.GetMSOwinUser : {user.Identity.Name}";
            };
        }
    }
}