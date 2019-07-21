using System.Data.Entity;

using ConnectionCheckerLibrary.DataBase.Models;

namespace ConnectionCheckerLibrary.DataBase.Context
{
    /// <summary>
    /// The connection checker db context.
    /// </summary>
    public class ConnectionCheckerDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        public DbSet<Connection> Connection { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionCheckerDbContext"/> class.
        /// </summary>
        public ConnectionCheckerDbContext() : base("ConnectionChecker")
        {
            Database.SetInitializer(new ConnectionCheckerDbInitializer());
        }

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>().ToTable("Connection");
            modelBuilder.Entity<Connection>().HasKey(connection => new { connection.URL });


            modelBuilder.Entity<Connection>().Property(connection => connection.CheckDelay).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
