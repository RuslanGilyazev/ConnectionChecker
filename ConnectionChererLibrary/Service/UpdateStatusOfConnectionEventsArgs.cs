using System;

using ConnectionCheckerLibrary.Service.Models;

namespace ConnectionCheckerLibrary.Service
{
    /// <summary>
    /// The update status of connection events args.
    /// </summary>
    public class UpdateStatusOfConnectionEventsArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the connection status.
        /// </summary>
        public ConnectionStatus ConnectionStatus { get; set; }
    }
}
