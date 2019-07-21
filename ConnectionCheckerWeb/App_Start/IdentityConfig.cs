using ConnectionChecker.Infrastructure;
using ConnectionCheckerLibrary.DataBase.Context;

using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace ConnectionChecker
{
    /// <summary>
    /// The identity config.
    /// </summary>
    public class IdentityConfig
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new ApplicationUserDbContext());
            app.CreatePerOwinContext<ApplicationUserRepository>(ApplicationUserRepository.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/User/Login"),
            });
        }
    }
}