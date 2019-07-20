using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionCheckerLibrary.DataBase.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ConnectionCheckerLibrary.DataBase.Context
{
    public class ApplicationUserDbInitializer : CreateDatabaseIfNotExists<ApplicationUserDbContext>
    {
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
