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

namespace ConnectionChecker.Controllers
{
    public class AdminPanelController : Controller
    {
        private ConnectionRepository _connectionRepository;
        private IConnectionCheckerService _connectionCheckerService;

        public AdminPanelController(ConnectionRepository connectionRepository, IConnectionCheckerService connectionCheckerService)
        {
            _connectionRepository = connectionRepository;
            _connectionCheckerService = connectionCheckerService;
        }

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

        [HttpPost]
        public ActionResult DeleteConnection(string url)
        {
            Connection connection = _connectionRepository.GetById(url);
            _connectionCheckerService.RemoveConnection(connection);

            _connectionRepository.Delete(url);
            _connectionRepository.Save();


            return Redirect(Url.Action("Index", "AdminPanel"));
        }

        [HttpPost]
        public ActionResult UpdateConnection(string url, bool enable)
        {
            Connection connection = _connectionRepository.GetById(url);
            connection.IsOn = enable;
            _connectionRepository.Save();

            return Redirect(Url.Action("Index", "AdminPanel"));
        }

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