using System.Security.Principal;
using System.Web.Security;

namespace ConnectionChecker.Tests.Models
{
    /// <summary>
    /// The current user.
    /// </summary>
    public class CurrentUser
    {
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Gets a value indicating whether is authenticated.
        /// </summary>
        public bool IsAuthenticated { get; private set; }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        public int UserID { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUser"/> class.
        /// </summary>
        /// <param name="identity">
        /// The identity.
        /// </param>
        public CurrentUser(IIdentity identity)
        {
            IsAuthenticated = identity.IsAuthenticated;
            DisplayName = identity.Name;

            var formsIdentity = identity as FormsIdentity;

            if (formsIdentity != null)
            {
                UserID = int.Parse(formsIdentity.Ticket.UserData);
            }
        }
    }
}
