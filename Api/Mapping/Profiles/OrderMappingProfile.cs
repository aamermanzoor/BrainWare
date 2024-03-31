namespace Api.Mapping.Profiles
{
    using Api.Data.Entity;
    using Api.Models;
    using AutoMapper;

    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderModel>()
            .ForMember(dest => dest.OrderTotal, opt => opt.MapFrom(src => src.Products.Sum(p => p.Quantity * p.Price)))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<OrderProduct, OrderProductModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
