using AutoMapper;
using WebShopAPI.Application.DTOs;
using WebShopAPI.Core.Entities;

namespace WebShopAPI.Helpers.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderProduct, OrderProductDTO>();
            CreateMap<Customer, CustomerDTO>();
        }
    }
}
