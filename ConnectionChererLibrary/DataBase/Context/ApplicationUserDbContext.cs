using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionCheckerLibrary.DataBase.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ConnectionCheckerLibrary.DataBase.Context
{
    public class ApplicationUserDbContext : IdentityDbContext<ApplicationUser>, IDisposable
    {
        public ApplicationUserDbContext() : base("ConnectionChecker")
        {
            Database.SetInitializer(new ApplicationUserDbInitializer());
        }
    }
}
