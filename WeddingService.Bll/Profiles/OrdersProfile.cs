using AutoMapper;
using WeddingService.Bll.Models;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Profiles;

/// <summary>
///     Orders Profile for AutoMapper
/// </summary>
public sealed class OrdersProfile : Profile
{
    /// <summary>
    ///     Constructor for profiler
    /// </summary>
    public OrdersProfile()
    {
        CreateMap<Order, OrdesDto>().ReverseMap();
        CreateMap<Order, OrdesDto>();
        CreateMap<OrdesDto, Order>();
    }
}
