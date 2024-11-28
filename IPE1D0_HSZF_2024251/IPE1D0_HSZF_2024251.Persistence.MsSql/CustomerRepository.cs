using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Model;
using IPE1D0_HSZF_2024251.Persistence.MsSql.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CarsharingDbContext _context;

        public CustomerRepository(CarsharingDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddCustomersAsync(IEnumerable<Customer> customers)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Enable IDENTITY_INSERT for the Customer table
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT customer ON");

                    foreach (var customer in customers)
                    {
                        var existingCustomer = await _context.Customer.FindAsync(customer.Id);

                        if (existingCustomer != null)
                        {
                            // Update existing record
                            existingCustomer.Name = customer.Name;
                            existingCustomer.Balance = customer.Balance;
                            _context.Customer.Update(existingCustomer);
                        }
                        else
                        {
                            // Add new record
                            await _context.Customer.AddAsync(customer);
                        }
                    }

                    await _context.SaveChangesAsync();

                    // Disable IDENTITY_INSERT
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT customer OFF");

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customer.FindAsync(id);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _context.Customer.FindAsync(customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Balance = customer.Balance;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddCustomerWithCustomIdAsync(Customer customer)
        {
            customer.Id = await GetMaxCustomerIdAsync() + 1;

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT customer ON");

                await _context.Customer.AddAsync(customer);
                await _context.SaveChangesAsync();

                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT customer OFF");

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> GetMaxCustomerIdAsync()
        {
            return await _context.Customer.MaxAsync(c => (int?)c.Id) ?? 0;
        }

        public async Task UpdateCustomerBalanceAsync(Customer customer)
        {
            var existingCustomer = await _context.Customer.FindAsync(customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.Balance = customer.Balance;
                await _context.SaveChangesAsync();
            }
        }


    }
}
