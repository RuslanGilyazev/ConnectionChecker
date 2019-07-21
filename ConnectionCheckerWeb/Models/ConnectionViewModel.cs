using System.Collections.Generic;

using ConnectionCheckerLibrary.DataBase.Models;

namespace ConnectionChecker.Models
{
    /// <summary>
    /// The connection view model.
    /// </summary>
    public class ConnectionViewModel
    {
        /// <summary>
        /// Gets or sets the connections.
        /// </summary>
        public List<Connection> Connections { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModel"/> class.
        /// </summary>
        /// <param name="connections">
        /// The connections.
        /// </param>
        public ConnectionViewModel(List<Connection> connections)
        {
            this.Connections = connections;
        }
    }
}