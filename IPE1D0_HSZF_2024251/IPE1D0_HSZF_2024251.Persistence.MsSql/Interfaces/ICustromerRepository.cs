using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPE1D0_HSZF_2024251.Model;

namespace IPE1D0_HSZF_2024251.Persistence.MsSql.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task AddCustomersAsync(IEnumerable<Customer> customers);
        Task AddCustomerAsync(Customer customer);
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
        Task AddCustomerWithCustomIdAsync(Customer customer);
        Task UpdateCustomerBalanceAsync(Customer customer);
    }
}
