using ConnectionCheckerLibrary.DataBase.Context;
using ConnectionCheckerLibrary.DataBase.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace ConnectionChecker.Infrastructure
{
    /// <summary>
    /// The application user repository.
    /// </summary>
    public class ApplicationUserRepository : UserManager<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserRepository"/> class.
        /// </summary>
        /// <param name="store">
        /// The store.
        /// </param>
        public ApplicationUserRepository(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        // this method is called by Owin therefore this is the best place to configure your User Manager
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationUserRepository"/>.
        /// </returns>
        public static ApplicationUserRepository Create(
            IdentityFactoryOptions<ApplicationUserRepository> options, IOwinContext context)
        {
            var manager = new ApplicationUserRepository(
                new UserStore<ApplicationUser>(context.Get<ApplicationUserDbContext>()));

            return manager;
        }
    }
}
