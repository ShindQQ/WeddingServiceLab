using AutoMapper;
using WeddingService.Bll.Models;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Profiles;

/// <summary>
///     Ceremonies Profile for AutoMapper
/// </summary>
public sealed class CeremoniesProfile : Profile
{
    /// <summary>
    ///     Constructor for profiler
    /// </summary>
    public CeremoniesProfile()
    {
        CreateMap<Ceremonies, CeremoniesDto>().ReverseMap();
        CreateMap<Ceremonies, CeremoniesDto>();
        CreateMap<CeremoniesDto, Ceremonies>();
    }
}
