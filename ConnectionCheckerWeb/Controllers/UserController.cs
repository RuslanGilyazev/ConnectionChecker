using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectionChecker.Infrastructure;
using ConnectionChecker.Models;
using ConnectionCheckerLibrary.DataBase.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace ConnectionChecker.Controllers
{
    /// <summary>
    /// The user controller.
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// The login.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(Url.Action("Index", "AdminPanel"));
            }
            else
            {
                LoginViewModel login = new LoginViewModel();

                return View(login);
            }
        }

        /// <summary>
        /// The sign out.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult SignOut()
        {
            Request.GetOwinContext()
                .Authentication
                .SignOut(HttpContext.GetOwinContext()
                    .Authentication.GetAuthenticationTypes()
                    .Select(o => o.AuthenticationType).ToArray());

            return Redirect(Url.Action("Index", "Connection"));
        }

        /// <summary>
        /// The login.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserRepository>();

                var authManager = HttpContext.GetOwinContext().Authentication;

                ApplicationUser user = userManager.Find(login.UserName, login.Password);

                if (user != null)
                {
                    var ident = userManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    //use the instance that has been created. 
                    authManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);

                    return Redirect(login.ReturnUrl ?? Url.Action("Index", "Connection"));
                }
            }

            ModelState.AddModelError("", "Invalid username or password");

            return View(login);
        }
    }
}