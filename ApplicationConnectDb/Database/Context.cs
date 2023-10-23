using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ApplicationConnectDb.Database.Models;
using ApplicationConnectDb.Database.ModelsConfigurations;

namespace ApplicationConnectDb.Database
{
    internal class Context : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }


        public Context()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new MySqlConnectionStringBuilder()
            {
                Server = "localhost",
                Database = "kolledj",
                Port = 3306,
                UserID = "root",
                Password = "",
            };
            optionsBuilder.UseMySQL(connectionString.ConnectionString)
                .LogTo(str => Debug.WriteLine(str), new[] { RelationalEventId.CommandExecuted })
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroupConfig());
            modelBuilder.ApplyConfiguration(new StudentConfig());
        }
    }
}
