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
            Database.Migrate();
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet <Trip> Trip { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>()
                .ToTable("trip") 
                .HasKey(t => t.Id);

            modelBuilder.Entity<Trip>()
                .HasOne<Car>()
                .WithMany()
                .HasForeignKey(t => t.CarId);

            modelBuilder.Entity<Trip>()
                .HasOne<Customer>()
                .WithMany()
                .HasForeignKey(t => t.CustomerId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=CarSharingDb;Integrated Security=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connStr);
            base.OnConfiguring(optionsBuilder);
        }

       
    }
    
}
