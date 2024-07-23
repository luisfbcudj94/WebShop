using WebShopAPI.Application.DTOs;
using WebShopAPI.Application.Interfaces;
using WebShopAPI.Core.Interfaces;

namespace WebShopAPI.Application.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetCustomersAsync();
            return customers.Select(c => new CustomerDTO
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email
            });
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null) return null;

            return new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };
        }
    }
}
