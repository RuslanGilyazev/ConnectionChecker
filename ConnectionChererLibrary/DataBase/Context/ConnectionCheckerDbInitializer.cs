using System.Collections.Generic;
using System.Data.Entity;

using ConnectionCheckerLibrary.DataBase.Models;

namespace ConnectionCheckerLibrary.DataBase.Context
{
    /// <summary>
    /// The connection checker db initializer.
    /// </summary>
    public class ConnectionCheckerDbInitializer : CreateDatabaseIfNotExists<ConnectionCheckerDbContext>
    {
        /// <summary>
        /// Generate the database fields
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected override void Seed(ConnectionCheckerDbContext context)
        {
            GetConnections().ForEach(connection => context.Connection.Add(connection));
            context.SaveChanges();

            base.Seed(context);
        }

        /// <summary>
        /// The get connections.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
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
