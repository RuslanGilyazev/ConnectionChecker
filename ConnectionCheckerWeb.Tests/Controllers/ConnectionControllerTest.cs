using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

using ConnectionChecker.Controllers;
using ConnectionChecker.Models;

using ConnectionCheckerLibrary.DataBase.Context;
using ConnectionCheckerLibrary.DataBase.Models;
using ConnectionCheckerLibrary.DataBase.Repository;
using ConnectionCheckerLibrary.Service;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ConnectionChecker.Tests.Controllers
{
    /// <summary>
    /// The connection controller test.
    /// </summary>
    [TestClass]
    public class ConnectionControllerTest : BaseControllerTest
    {
        /// <summary>
        /// The test index view.
        /// </summary>
        [TestMethod]
        public void TestIndexView()
        {
            _connectionController.Index();
            PartialViewResult actionResult = _connectionController.GetConnectionList() as PartialViewResult;

            Assert.IsNotNull(actionResult, "Connection list is null");

            ConnectionStatusViewModel connectionStatusViewModel = actionResult.Model as ConnectionStatusViewModel;


            Assert.IsNotNull(connectionStatusViewModel, "Incorrect model view");

            int countOfStatuses = _connectionCheckerService.ConnectionStatusStates.Count;

            Assert.AreEqual(
                countOfStatuses,
                connectionStatusViewModel.ConnectionStatuses.Count,
                "Connection list don't show all connections");
        }
    }
}
