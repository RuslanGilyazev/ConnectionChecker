using Microsoft.AspNet.Identity.EntityFramework;

namespace ConnectionCheckerLibrary.DataBase.Models
{
    public class ApplicationUserRole : IdentityRole
    {
        public ApplicationUserRole() : base() { }

        public ApplicationUserRole(string name) : base(name) { }
    }
}
