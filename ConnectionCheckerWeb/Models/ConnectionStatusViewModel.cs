using System.Collections.Generic;

using ConnectionCheckerLibrary.Service.Models;

namespace ConnectionChecker.Models
{
    /// <summary>
    /// The connection status view model.
    /// </summary>
    public class ConnectionStatusViewModel
    {
        /// <summary>
        /// Gets or sets the connection statuses.
        /// </summary>
        public List<ConnectionStatus> ConnectionStatuses { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStatusViewModel"/> class.
        /// </summary>
        /// <param name="connectionStatuses">
        /// The connection statuses.
        /// </param>
        public ConnectionStatusViewModel(List<ConnectionStatus> connectionStatuses)
        {
            ConnectionStatuses = connectionStatuses;
        }
    }
}