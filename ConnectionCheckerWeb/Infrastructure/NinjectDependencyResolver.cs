using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ConnectionCheckerLibrary.DataBase.Context;
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

        public void Bind()
        {
            _kernel.Bind<ConnectionRepository>()
                .ToSelf()
                .InSingletonScope()
                .WithParameter(
                    new Parameter(
                        "context",
                        new ConnectionCheckerDbContext(),
                        false));

            _kernel.Bind<IConnectionCheckerService>()
                .To<ConnectionCheckerService>()
                .InSingletonScope()
                .WithConstructorArgument(
                    new Parameter("connectionRepository", 
                        _kernel.Get<ConnectionRepository>(), 
                        false));

            _kernel.Get<IConnectionCheckerService>().StartConnectionChecking(CancellationToken.None);
        }
    }
}