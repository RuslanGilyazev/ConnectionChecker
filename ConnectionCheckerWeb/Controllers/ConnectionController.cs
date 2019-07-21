using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ConnectionChecker.Models;
using ConnectionCheckerLibrary.DataBase.Models;
using ConnectionCheckerLibrary.DataBase.Repository;
using ConnectionCheckerLibrary.Service;
using ConnectionCheckerLibrary.Service.Models;

namespace ConnectionChecker.Controllers
{
    /// <summary>
    /// The connection controller.
    /// </summary>
    public class ConnectionController : Controller
    {
        /// <summary>
        /// The connection repository.
        /// </summary>
        private IBaseRepository<Connection> _connectionRepository;

        /// <summary>
        /// The connection checker service.
        /// </summary>
        private IConnectionCheckerService _connectionCheckerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionController"/> class.
        /// </summary>
        /// <param name="connectionRepository">
        /// The connection repository.
        /// </param>
        /// <param name="connectionCheckerService">
        /// The connection checker service.
        /// </param>
        public ConnectionController(IBaseRepository<Connection> connectionRepository, IConnectionCheckerService connectionCheckerService)
        {
            _connectionRepository = connectionRepository;
            _connectionCheckerService = connectionCheckerService;
        }

        /// <summary>
        /// The main page of connections.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The get connection list.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
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