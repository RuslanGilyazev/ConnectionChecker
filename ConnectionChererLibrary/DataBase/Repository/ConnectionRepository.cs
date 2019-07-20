using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionCheckerLibrary.DataBase.Context;
using ConnectionCheckerLibrary.DataBase.Models;

namespace ConnectionCheckerLibrary.DataBase.Repository
{
    public class ConnectionRepository : BaseRepository<Connection>
    {
        public new ConnectionCheckerDbContext DbContext => (ConnectionCheckerDbContext) base.DbContext;

        public ConnectionRepository() : base(new ConnectionCheckerDbContext())
        {
        }

        public ConnectionRepository(ConnectionCheckerDbContext context) : base(context)
        {
        }

        public override IEnumerable<Connection> GetAll()
        {
            return DbContext.Connection;
        }

        public override Connection GetById(params object[] key)
        {
            return DbContext.Connection.Find(key);
        }
         
        public override void Insert(Connection newInstance)
        {
            DbContext.Connection.Add(newInstance);
        }

        public override void Update(Connection existInstance)
        {
            DbContext.Entry(existInstance).State = EntityState.Modified;
        }

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
