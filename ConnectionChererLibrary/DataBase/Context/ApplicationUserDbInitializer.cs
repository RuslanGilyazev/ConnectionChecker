using System.Data.Entity;

using ConnectionCheckerLibrary.DataBase.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ConnectionCheckerLibrary.DataBase.Context
{
    /// <summary>
    /// The application user db initializer.
    /// </summary>
    public class ApplicationUserDbInitializer : CreateDatabaseIfNotExists<ApplicationUserDbContext>
    {
        /// <summary>
        /// Generate database fields
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected override void Seed(ApplicationUserDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.UserName = "Admin";
            userManager.Create(applicationUser, "123456");

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
