using System;
using System.Data.Entity;

using ConnectionCheckerLibrary.DataBase.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ConnectionCheckerLibrary.DataBase.Context
{
    /// <summary>
    /// The application user db context.
    /// </summary>
    public class ApplicationUserDbContext : IdentityDbContext<ApplicationUser>, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserDbContext"/> class.
        /// </summary>
        public ApplicationUserDbContext() : base("ConnectionChecker")
        {
            Database.SetInitializer(new ApplicationUserDbInitializer());
        }
    }
}
