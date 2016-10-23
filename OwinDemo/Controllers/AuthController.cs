using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OwinDemo.Models;
using System.Security.Claims;

namespace OwinDemo.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            var vm = new LoginViewModel();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var identity = new ClaimsIdentity("AppicationCookie");
                var fakeSessionId = Guid.NewGuid().ToString();
                identity.AddClaims(new List<Claim>
                {
                    new Claim("FakeSessionId", fakeSessionId),
                    new Claim(ClaimTypes.NameIdentifier, model.UserName),
                    //new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", fakeSessionId),
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Hash, model.Password)
                });
                HttpContext.GetOwinContext().Authentication.SignIn(identity);
                return Redirect(ReturnUrl);
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}