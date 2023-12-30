using AutoMapper;
using Talabat.API.DTOs;
using Talabat.Core.Enitites;
using Talabat.Core.Enitites.Identitiy;
using Talabat.Core.Enitites.Order_Aggregate;

namespace Talabat.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Product, ProductToreturnDTO>()
                .ForMember(d => d.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(d => d.ProductType, O => O.MapFrom(S => S.ProductType.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlReSolver>());

            CreateMap<AddressDTO,Address>().ReverseMap();

            CreateMap<CustomerBasket,CustomerBasketDTO>().ReverseMap();
            CreateMap<BasketItem,BasketItemDTO>().ReverseMap();

            CreateMap<AddressDTO,OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDTO>()
                     .ForMember(d => d.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                     .ForMember(d => d.DeliveryMethodCost, O => O.MapFrom(S => S.DeliveryMethod.Cost));


            CreateMap<OrderItem, OrderItemDTO>()
                     .ForMember(d => d.ProductId, O => O.MapFrom(S => S.Product.ProductId))
                     .ForMember(d => d.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                     .ForMember(d => d.PictureUrl, O => O.MapFrom(S => S.Product.PictureUrl))
                      .ForMember(d => d.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());
        }
    }
}
