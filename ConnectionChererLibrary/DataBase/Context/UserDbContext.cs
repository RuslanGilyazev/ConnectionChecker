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
    public class UserDbContext : IdentityDbContext<User>, IDisposable
    {
        public UserDbContext()
            : base("User", throwIfV1Schema: false)
        {
        }

        public static UserDbContext Create()
        {
            return new UserDbContext();
        }
    }
}
