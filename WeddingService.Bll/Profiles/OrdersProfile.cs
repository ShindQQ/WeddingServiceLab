using AutoMapper;
using WeddingService.Bll.Models;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Profiles;

public sealed class OrdersProfile : Profile
{
    public OrdersProfile()
    {
        CreateMap<Orders, OrdersDto>().ReverseMap();
        CreateMap<Orders, OrdersDto>();
        CreateMap<OrdersDto, Orders>();
    }
}
