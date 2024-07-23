using WebShopAPI.Application.DTOs;

namespace WebShopAPI.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetCustomersAsync();
        Task<CustomerDTO> GetCustomerByIdAsync(Guid customerId);
    }
}
