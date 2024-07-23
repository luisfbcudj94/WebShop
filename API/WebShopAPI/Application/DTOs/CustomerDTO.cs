namespace WebShopAPI.Application.DTOs
{
    public class CustomerDTO
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
