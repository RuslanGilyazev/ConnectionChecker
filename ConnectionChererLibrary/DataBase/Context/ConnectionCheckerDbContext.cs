using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionCheckerLibrary.DataBase.Models;

namespace ConnectionCheckerLibrary.DataBase.Context
{
    public class ConnectionCheckerDbContext : DbContext
    {
        public DbSet<Connection> Connection { get; set; }

        public ConnectionCheckerDbContext() : base("ConnectionChecker")
        {
            Database.SetInitializer(new ConnectionCheckerDbInitializer());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>().ToTable("Connection");
            modelBuilder.Entity<Connection>().HasKey(connection => new { connection.URL });


            modelBuilder.Entity<Connection>().Property(connection => connection.CheckDelay).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
