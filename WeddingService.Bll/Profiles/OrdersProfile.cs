using AutoMapper;
using WeddingService.Bll.Models;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Profiles;

/// <summary>
///     Orders Profile for AutoMapper
/// </summary>
public sealed class OrderProfile : Profile
{
    /// <summary>
    ///     Constructor for profiler
    /// </summary>
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderDto, Order>();
    }
}
