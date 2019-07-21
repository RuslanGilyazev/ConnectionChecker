using Microsoft.AspNet.Identity.EntityFramework;

namespace ConnectionCheckerLibrary.DataBase.Models
{
    /// <summary>
    /// The application user.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
