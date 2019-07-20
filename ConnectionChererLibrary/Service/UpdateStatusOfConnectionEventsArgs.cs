using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionCheckerLibrary.DataBase.Models;
using ConnectionCheckerLibrary.Service.Models;

namespace ConnectionCheckerLibrary.Service
{
    public class UpdateStatusOfConnectionEventsArgs : EventArgs
    {
        public ConnectionStatus ConnectionStatus { get; set; }
    }
}
