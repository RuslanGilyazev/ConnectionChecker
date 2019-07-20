using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionCheckerLibrary.DataBase.Models;

namespace ConnectionCheckerLibrary.DataBase.Context
{
    public class ConnectionCheckerDbInitializer : CreateDatabaseIfNotExists<ConnectionCheckerDbContext>
    {
        /// <summary>
        /// A method that should be overridden to actually add data to the context for seeding.
        /// The default implementation does nothing.
        /// </summary>
        /// <param name="context"> The context to seed. </param>
        protected override void Seed(ConnectionCheckerDbContext context)
        {
            GetConnections().ForEach(connection => context.Connection.Add(connection));
            context.SaveChanges();

            base.Seed(context);
        }

        public List<Connection> GetConnections()
        {
            return new List<Connection>()
            {
                new Connection()
                {
                    URL = "http://google.com",
                    CheckDelay = 10,
                    IsOn = true
                },
                new Connection()
                {
                    URL = "http://1211221211221122121.com",
                    CheckDelay = 15,
                    IsOn = true
                },
                new Connection()
                {
                    URL = "http://yandex.ru",
                    CheckDelay = 20,
                    IsOn = false
                },
                new Connection()
                {
                    URL = "http://localhost",
                    CheckDelay = 100,
                    IsOn = true
                }
            };
        }
    }
}
