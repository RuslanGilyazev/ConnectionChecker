using ConnectionCheckerLibrary.DataBase.Context;
using ConnectionCheckerLibrary.DataBase.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace ConnectionChecker.Infrastructure
{
    public class ApplicationUserRepository : UserManager<ApplicationUser>
    {
        public ApplicationUserRepository(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        // this method is called by Owin therefore this is the best place to configure your User Manager
        public static ApplicationUserRepository Create(
            IdentityFactoryOptions<ApplicationUserRepository> options, IOwinContext context)
        {
            var manager = new ApplicationUserRepository(
                new UserStore<ApplicationUser>(context.Get<ApplicationUserDbContext>()));

            // optionally configure your manager
            // ...

            return manager;
        }
    }
}
