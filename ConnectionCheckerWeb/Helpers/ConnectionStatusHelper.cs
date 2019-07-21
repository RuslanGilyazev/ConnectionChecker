using ConnectionCheckerLibrary.Service.Models;

namespace ConnectionChecker.Helpers
{
    /// <summary>
    /// The connection status helper.
    /// </summary>
    public class ConnectionStatusHelper
    {
        /// <summary>
        /// The connection status state class.
        /// </summary>
        /// <param name="connectionStatusState">
        /// The connection status state.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ConnectionStatusStateClass(ConnectionStatusState connectionStatusState)
        {
            switch (connectionStatusState)
            {
                case ConnectionStatusState.Available:
                {
                    return "success";
                }

                case ConnectionStatusState.Unavailable:
                {
                    return "danger";
                }

                case ConnectionStatusState.Started:
                {
                    return "info";
                }

                case ConnectionStatusState.Off:
                {
                    return "secondary";
                }

                default:
                {
                    return "primary";
                }
            }
        }
    }
}