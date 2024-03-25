using Microsoft.EntityFrameworkCore;
using NEOBANK.WEBAPI.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEOBANK.WEBAPI.DataAccess
{
    public class DatabaseContext : DbContext
    {
        private string _connectionString;

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<LoginModel> LoginData { get; set; }
        public DbSet<AuthModel> AuthData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
