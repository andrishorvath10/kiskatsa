using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Model;
using Microsoft.EntityFrameworkCore;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public class CarsharingDbContext : DbContext
    {
        public CarsharingDbContext(DbContextOptions<CarsharingDbContext> options) : base(options) { }
        public CarsharingDbContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Car> cars { get; set; }
        public DbSet<Customer> customer { get; set; }
        public DbSet <Trip> trip { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=CarSharingDb;Integrated Security=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connStr);
            base.OnConfiguring(optionsBuilder);
        }

       
    }
    
}
