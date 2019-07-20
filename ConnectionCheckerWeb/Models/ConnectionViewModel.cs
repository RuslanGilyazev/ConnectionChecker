using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConnectionCheckerLibrary.DataBase.Models;

namespace ConnectionChecker.Models
{
    public class ConnectionViewModel
    {

        public List<Connection> Connections { get; set; }

        public ConnectionViewModel(List<Connection> connections)
        {
            this.Connections = connections;
        }
    }
}