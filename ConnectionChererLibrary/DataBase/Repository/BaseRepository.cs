using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionCheckerLibrary.DataBase.Repository
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetById(params object[] key);

        void Insert(T newInstance);

        void Update(T existInstance);

        void Delete(params object[] key);

        void Save();
    }

    public abstract class BaseRepository<T> : IBaseRepository<T>
    {
        private readonly DbContext _dbContext;

        public DbContext DbContext => _dbContext;

        protected BaseRepository(DbContext context)
        {
            _dbContext = context;
        }

        public abstract IEnumerable<T> GetAll();

        public abstract T GetById(params object[] key);

        public abstract void Insert(T newInstance);

        public abstract void Update(T existInstance);

        public abstract void Delete(params object[] key);

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
    }
}
