using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ConnectionCheckerLibrary.DataBase.Context;
using ConnectionCheckerLibrary.DataBase.Models;
using ConnectionCheckerLibrary.DataBase.Repository;
using ConnectionCheckerLibrary.Service;
using Ninject;
using Ninject.Parameters;

namespace ConnectionChecker.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;

            Bind();
        }

        /// <summary>Resolves singly registered services that support arbitrary object creation.</summary>
        /// <returns>The requested service or object.</returns>
        /// <param name="serviceType">The type of the requested service or object.</param>
        public object GetService(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        /// <summary>Resolves multiply registered services.</summary>
        /// <returns>The requested services.</returns>
        /// <param name="serviceType">The type of the requested services.</param>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        /// <summary>
        /// The bind.
        /// </summary>
        public void Bind()
        {
            _kernel.Bind<ConnectionCheckerDbContext>().ToSelf().InSingletonScope();

            _kernel.Bind<IBaseRepository<Connection>>()
                .To<BaseRepository<Connection>>()
                .InSingletonScope();

            _kernel.Get<IBaseRepository<Connection>>().DbContext = _kernel.Get<ConnectionCheckerDbContext>();
            _kernel.Get<IBaseRepository<Connection>>().DbSet = _kernel.Get<ConnectionCheckerDbContext>().Connection;

            _kernel.Bind<IConnectionCheckerService>()
                .To<ConnectionCheckerService>()
                .InSingletonScope()
                .WithConstructorArgument(
                    new Parameter("connectionRepository", 
                        _kernel.Get<IBaseRepository<Connection>>(), 
                        false));

            _kernel.Get<IConnectionCheckerService>().StartConnectionCheck(CancellationToken.None);
        }
    }
}