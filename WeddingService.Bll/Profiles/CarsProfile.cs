using AutoMapper;
using WeddingService.Bll.Models;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Profiles;

/// <summary>
///     Cars Profile for AutoMapper
/// </summary>
public sealed class CarsProfile : Profile
{
    /// <summary>
    ///     Constructor for profiler
    /// </summary>
    public CarsProfile()
    {
        CreateMap<Cars, CarsDto>().ReverseMap();
        CreateMap<Cars, CarsDto>();
        CreateMap<CarsDto, Cars>();
    }
}
