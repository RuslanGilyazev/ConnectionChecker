using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ConnectionChecker.Models;
using ConnectionCheckerLibrary.DataBase.Models;
using ConnectionCheckerLibrary.DataBase.Repository;
using ConnectionCheckerLibrary.Service;
using ConnectionCheckerLibrary.Service.Models;

namespace ConnectionChecker.Controllers
{
    public class ConnectionController : Controller
    {
        private ConnectionRepository _connectionRepository;
        private IConnectionCheckerService _connectionCheckerService;

        public ConnectionController(ConnectionRepository connectionRepository, IConnectionCheckerService connectionCheckerService)
        {
            _connectionRepository = connectionRepository;
            _connectionCheckerService = connectionCheckerService;
        }

        // GET: Connection
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetConnectionList()
        {
            List<ConnectionStatus> connectionStatuses = new List<ConnectionStatus>();

            foreach (KeyValuePair<Connection, ConnectionStatus> keyValuePair in _connectionCheckerService.ConnectionStatusStates.ToArray().ToList())
            {
                connectionStatuses.Add(keyValuePair.Value);
            }
            ConnectionStatusViewModel connectionStatusViewModel = new ConnectionStatusViewModel(connectionStatuses);

            return PartialView("List", connectionStatusViewModel);
        }

    }
}