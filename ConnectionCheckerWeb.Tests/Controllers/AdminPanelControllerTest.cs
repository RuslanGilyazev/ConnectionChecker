using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using ConnectionChecker.Controllers;
using ConnectionChecker.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ConnectionChecker.Tests.Controllers
{
    /// <summary>
    /// The admin panel controller test.
    /// </summary>
    [TestClass]
    public class AdminPanelControllerTest : BaseControllerTest
    {       
        /// <summary>
        /// The _connection controller.
        /// </summary>
        protected AdminPanelController _adminPanelController;

        /// <summary>
        /// The test initialize.
        /// </summary>
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            _adminPanelController = new AdminPanelController(_connectionRepository, _connectionCheckerService);
        }

        /// <summary>
        /// The test index view.
        /// </summary>
        [TestMethod]
        public void TestIndexView()
        {
            UserAuthenticate(_adminPanelController);

            ViewResult actionResult = _adminPanelController.Index() as ViewResult;

            ConnectionViewModel connectionViewModel = actionResult.Model as ConnectionViewModel;

            int countOfStatuses = _connectionCheckerService.ConnectionStatusStates.Count;
            int connectionsCount = connectionViewModel.Connections.Count;

            Assert.AreEqual(connectionsCount, connectionsCount, "Some of connections are not showing");
        }
    }
}
