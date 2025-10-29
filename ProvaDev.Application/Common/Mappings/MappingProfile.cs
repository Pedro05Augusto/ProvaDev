using AutoMapper;
using ProvaDev.Application.DTOs;
using ProvaDev.Domain.Entities.Customers;
using ProvaDev.Domain.Entities.Orders;
using ProvaDev.Domain.Entities.OrderItems;
using ProvaDev.Domain.Entities.Products;

namespace ProvaDev.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDto>();
        
        CreateMap<Product, ProductDto>();
        
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.CustomerName, 
                opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : string.Empty))
            .ForMember(dest => dest.Items, 
                opt => opt.MapFrom(src => src.Items));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductName, 
                opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
            .ForMember(dest => dest.TotalPrice, 
                opt => opt.MapFrom(src => src.GetTotalPrice()));
    }
}
