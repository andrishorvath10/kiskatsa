using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Model;
using Microsoft.EntityFrameworkCore;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CarsharingDbContext _dbContext;

        public CustomerRepository(CarsharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCustomersAsync(IEnumerable<Customer> customers)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Enable IDENTITY_INSERT for the Customer table
                    await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT customer ON");

                    foreach (var customer in customers)
                    {
                        var existingCustomer = await _dbContext.Customer.FindAsync(customer.Id);

                        if (existingCustomer != null)
                        {
                            // Update existing record
                            existingCustomer.Name = customer.Name;
                            existingCustomer.Balance = customer.Balance;
                            _dbContext.Customer.Update(existingCustomer);
                        }
                        else
                        {
                            // Add new record
                            await _dbContext.Customer.AddAsync(customer);
                        }
                    }

                    await _dbContext.SaveChangesAsync();

                    // Disable IDENTITY_INSERT
                    await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT customer OFF");

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
