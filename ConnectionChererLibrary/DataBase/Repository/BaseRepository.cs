using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ConnectionCheckerLibrary.DataBase.Repository
{
    /// <summary>
    /// The BaseRepository interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IBaseRepository<T>
    where T : class
    {
        /// <summary>
        /// Gets or sets the db context.
        /// </summary>
        DbContext DbContext { get; set; }

        /// <summary>
        /// Gets or sets the db set.
        /// </summary>
        DbSet<T> DbSet { get; set; }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IList<T> GetAll();

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
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        /// <summary>
        /// The _db context.
        /// </summary>
        private DbContext _dbContext;

        public DbContext DbContext
        {
            get => _dbContext;
            set => _dbContext = value;
        }

        /// <summary>
        /// The _db set.
        /// </summary>
        protected DbSet<T> _dbSet;

        /// <summary>
        /// Gets or sets the db set.
        /// </summary>
        public DbSet<T> DbSet
        {
            get => _dbSet;
            set => _dbSet = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public BaseRepository(DbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        public BaseRepository()
        {
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetById(params object[] key)
        {
            return DbSet.Find(key);
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public void Delete(params object[] key)
        {
            Delete(GetById(key));
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<T> GetAll()
        {
            return DbSet.ToList();
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

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
