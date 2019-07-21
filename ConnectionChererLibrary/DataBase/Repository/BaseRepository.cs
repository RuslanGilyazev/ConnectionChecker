using System.Collections.Generic;
using System.Data.Entity;

namespace ConnectionCheckerLibrary.DataBase.Repository
{
    /// <summary>
    /// The BaseRepository interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T GetById(params object[] key);

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="newInstance">
        /// The new instance.
        /// </param>
        void Insert(T newInstance);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="existInstance">
        /// The exist instance.
        /// </param>
        void Update(T existInstance);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        void Delete(params object[] key);

        /// <summary>
        /// The save.
        /// </summary>
        void Save();
    }

    /// <summary>
    /// The base repository.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract class BaseRepository<T> : DbSet<T>, IBaseRepository<T>
    {
        /// <summary>
        /// The _db context.
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// The db context.
        /// </summary>
        public DbContext DbContext => _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected BaseRepository(DbContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public abstract IEnumerable<T> GetAll();

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public abstract T GetById(params object[] key);

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="newInstance">
        /// The new instance.
        /// </param>
        public abstract void Insert(T newInstance);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="existInstance">
        /// The exist instance.
        /// </param>
        public abstract void Update(T existInstance);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public abstract void Delete(params object[] key);

        /// <summary>
        /// The save.
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
    }
}
