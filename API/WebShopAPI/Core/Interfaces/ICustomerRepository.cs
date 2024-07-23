using WebShopAPI.Core.Entities;

namespace WebShopAPI.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(Guid customerId);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid customerId);
    }
}
