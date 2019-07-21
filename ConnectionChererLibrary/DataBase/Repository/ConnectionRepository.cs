using System.Collections.Generic;
using System.Data.Entity;

using ConnectionCheckerLibrary.DataBase.Context;
using ConnectionCheckerLibrary.DataBase.Models;

namespace ConnectionCheckerLibrary.DataBase.Repository
{
    /// <summary>
    /// The connection repository.
    /// </summary>
    public class ConnectionRepository : BaseRepository<Connection>
    {
        /// <summary>
        /// The db context.
        /// </summary>
        public new ConnectionCheckerDbContext DbContext => (ConnectionCheckerDbContext) base.DbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionRepository"/> class.
        /// </summary>
        public ConnectionRepository() : base(new ConnectionCheckerDbContext())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public ConnectionRepository(ConnectionCheckerDbContext context) : base(context)
        {
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public override IEnumerable<Connection> GetAll()
        {
            return DbContext.Connection;
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="Connection"/>.
        /// </returns>
        public override Connection GetById(params object[] key)
        {
            return DbContext.Connection.Find(key);
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="newInstance">
        /// The new instance.
        /// </param>
        public override void Insert(Connection newInstance)
        {
            DbContext.Connection.Add(newInstance);
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="existInstance">
        /// The exist instance.
        /// </param>
        public override void Update(Connection existInstance)
        {
            DbContext.Entry(existInstance).State = EntityState.Modified;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public override void Delete(params object[] key)
        {
            Connection connection = GetById(key);

            if (connection != null)
            {
                DbContext.Connection.Remove(connection);
            }
        }
    }
}
