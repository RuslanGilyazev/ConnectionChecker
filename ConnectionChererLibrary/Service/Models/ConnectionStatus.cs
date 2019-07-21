using System;

using ConnectionCheckerLibrary.DataBase.Models;

namespace ConnectionCheckerLibrary.Service.Models
{
    /// <summary>
    /// The connection status state.
    /// </summary>
    public enum ConnectionStatusState
    {
        /// <summary>
        /// Connections is disabled
        /// </summary>
        Off,

        /// <summary>
        /// Connection is started
        /// </summary>
        Started,

        /// <summary>
        /// The connection is available.
        /// </summary>
        Available,

        /// <summary>
        /// The connection is unavailable.
        /// </summary>
        Unavailable,

        /// <summary>
        /// The connection is deleted.
        /// </summary>
        Deleted
    }

    /// <summary>
    /// The connection status.
    /// </summary>
    public class ConnectionStatus
    {
        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        public Connection Connection { get; set; }

        /// <summary>
        /// Gets or sets the connection status state.
        /// </summary>
        public ConnectionStatusState ConnectionStatusState { get; set; }

        /// <summary>
        /// Gets or sets the connection check date time.
        /// </summary>
        public DateTime ConnectionCheckDateTime { get; set; }

        /// <summary>
        /// Gets or sets the success connections count.
        /// </summary>
        public int SuccessConnectionsCount { get; set; }

        /// <summary>
        /// Gets or sets the bad connections count.
        /// </summary>
        public int BadConnectionsCount { get; set; }

        /// <summary>
        /// The connections count.
        /// </summary>
        public int ConnectionsCount => BadConnectionsCount + SuccessConnectionsCount;

        /// <summary>
        /// The success connections percent.
        /// </summary>
        public float SuccessConnectionsPercent =>
            (((float)SuccessConnectionsCount) / ConnectionsCount) * 100;
    }
}
