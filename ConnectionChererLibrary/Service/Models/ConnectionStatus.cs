using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionCheckerLibrary.DataBase.Models;

namespace ConnectionCheckerLibrary.Service.Models
{
    public enum ConnectionStatusState
    {
        Off, Started, Available, Unavailable, Deleted
    }

    public class ConnectionStatus
    {
        public Connection Connection { get; set; }

        public ConnectionStatusState ConnectionStatusState { get; set; }

        public DateTime ConnectionCheckDateTime { get; set; }

        public int SuccessConnectionsCount { get; set; }

        public int BadConnectionsCount { get; set; }

        public int ConnectionsCount => BadConnectionsCount + SuccessConnectionsCount;

        public float SuccessConnectionsPercent =>
            (((float)SuccessConnectionsCount) / ConnectionsCount) * 100;
    }
}
