using System.Collections.Generic;
using ConnectionCheckerLibrary.DataBase.Models;
using ConnectionCheckerLibrary.Service.Models;

namespace ConnectionChecker.Models
{
    public class ConnectionStatusViewModel
    {
        public List<ConnectionStatus> ConnectionStatuses { get; set; }

        public ConnectionStatusViewModel(List<ConnectionStatus> connectionStatuses)
        {
            ConnectionStatuses = connectionStatuses;
        }
    }
}