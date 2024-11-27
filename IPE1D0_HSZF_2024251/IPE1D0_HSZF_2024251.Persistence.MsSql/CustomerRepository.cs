using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Model;
using Microsoft.EntityFrameworkCore;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CarsharingDbContext _dbContext;

        public CustomerRepository(CarsharingDbContext carsharingDb)
        {
            _dbContext = carsharingDb;
        }

        public void AddCustomers(IEnumerable<Customer> customers)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Engedélyezd az IDENTITY_INSERT-et a Customer táblán
                    _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Customer ON");

                    // Adatok beszúrása
                    _dbContext.customer.AddRange(customers);
                    _dbContext.SaveChanges();

                    // Kapcsold ki az IDENTITY_INSERT-et
                    _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Customer OFF");

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw; // Újradobja a hibát
                }
            }
        }
    }
}
