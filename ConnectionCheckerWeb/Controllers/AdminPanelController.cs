using System.Linq;
using System.Web.Mvc;
using ConnectionChecker.Models;
using ConnectionCheckerLibrary.DataBase.Models;
using ConnectionCheckerLibrary.DataBase.Repository;
using ConnectionCheckerLibrary.Service;

namespace ConnectionChecker.Controllers
{
    /// <summary>
    /// The admin panel controller.
    /// This panel needed to deleting and enabling existing connections
    /// </summary>
    public class AdminPanelController : Controller
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
        /// Initializes a new instance of the <see cref="AdminPanelController"/> class.
        /// </summary>
        /// <param name="connectionRepository">
        /// The connection repository.
        /// </param>
        /// <param name="connectionCheckerService">
        /// The connection checker service.
        /// </param>
        public AdminPanelController(IBaseRepository<Connection> connectionRepository, IConnectionCheckerService connectionCheckerService)
        {
            _connectionRepository = connectionRepository;
            _connectionCheckerService = connectionCheckerService;
        }

        /// <summary>
        /// The main page of admin panel
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ConnectionViewModel connectionViewModel = new ConnectionViewModel(_connectionRepository.GetAll().ToList());

                return View(connectionViewModel);
            }
            else
            {
                return Redirect(Url.Action("Index", "Connection"));
            }
        }

        /// <summary>
        /// The delete connection.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult DeleteConnection(string url)
        {
            Connection connection = _connectionRepository.GetById(url);
            _connectionCheckerService.RemoveConnection(connection);

            _connectionRepository.Delete(url);
            _connectionRepository.Save();


            return Redirect(Url.Action("Index", "AdminPanel"));
        }

        /// <summary>
        /// The update connection.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="enable">
        /// The enable.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult UpdateConnection(string url, bool enable)
        {
            Connection connection = _connectionRepository.GetById(url);
            connection.IsOn = enable;
            _connectionRepository.Save();

            return Redirect(Url.Action("Index", "AdminPanel"));
        }

        /// <summary>
        /// The create connection page.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult CreateConnection()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect(Url.Action("Index", "Connection"));
            }
        }

        /// <summary>
        /// The create connection.
        /// </summary>
        /// <param name="connection">
        /// The connection.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult CreateConnection(Connection connection)
        {
            if (ModelState.IsValid)
            {
                if(_connectionRepository.GetById(connection.URL) == null)
                {
                    _connectionRepository.Insert(connection);
                    _connectionRepository.Save();

                    _connectionCheckerService.StartConnectionCheck(connection);
                }
                else
                {
                    ModelState.AddModelError("", "Site already added");

                    return View(connection);
                }
            }

            return Redirect(Url.Action("Index", "AdminPanel"));
        }
    }
}