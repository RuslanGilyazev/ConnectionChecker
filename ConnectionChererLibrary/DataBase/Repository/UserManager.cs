using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionCheckerLibrary.DataBase.Context;
using ConnectionCheckerLibrary.DataBase.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace ConnectionCheckerLibrary.DataBase.Repository
{
    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store)
            : base(store)
        {
        }

        // this method is called by Owin therefore this is the best place to configure your User Manager
        public static UserManager Create(
            IdentityFactoryOptions<UserManager> options, IOwinContext context)
        {
            var manager = new UserManager(
                new UserStore<User>(context.Get<UserDbContext>()));

            // optionally configure your manager
            // ...

            return manager;
        }
    }
}
