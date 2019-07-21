using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ConnectionChecker.Controllers;

using ConnectionCheckerLibrary.DataBase.Context;
using ConnectionCheckerLibrary.DataBase.Models;
using ConnectionCheckerLibrary.DataBase.Repository;
using ConnectionCheckerLibrary.Service;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ConnectionChecker.Tests.Controllers
{
    public class BaseControllerTest
    {
        /// <summary>
        /// The _connection checker service.
        /// </summary>
        protected ConnectionCheckerService _connectionCheckerService;

        /// <summary>
        /// The _connection controller.
        /// </summary>
        protected ConnectionController _connectionController;

        /// <summary>
        /// The connection repository.
        /// </summary>
        protected IBaseRepository<Connection> _connectionRepository;

        /// <summary>
        /// The test initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _connectionRepository = new BaseRepository<Connection>();
            Mock<DbSet<Connection>> mockConnectionDbSet = new Mock<DbSet<Connection>>() { CallBase = true };

            ConnectionCheckerDbInitializer connectionCheckerDbInitializer = new ConnectionCheckerDbInitializer();
            IQueryable<Connection> connections = connectionCheckerDbInitializer.GetConnections().AsQueryable();

            mockConnectionDbSet.As<IQueryable<Connection>>().Setup(m => m.Provider).Returns(connections.Provider);
            mockConnectionDbSet.As<IQueryable<Connection>>().Setup(m => m.Expression).Returns(connections.Expression);
            mockConnectionDbSet.As<IQueryable<Connection>>().Setup(m => m.ElementType).Returns(connections.ElementType);
            mockConnectionDbSet.As<IQueryable<Connection>>().Setup(m => m.GetEnumerator()).Returns(connections.GetEnumerator());

            _connectionRepository.DbSet = mockConnectionDbSet.Object;

            _connectionCheckerService = new ConnectionCheckerService(_connectionRepository);
            _connectionCheckerService.StartConnectionCheck(CancellationToken.None);

            _connectionController = new ConnectionController(_connectionRepository, _connectionCheckerService);
        }
    }
}
