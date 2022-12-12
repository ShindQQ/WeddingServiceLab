using AutoMapper;
using WeddingService.Bll.Models;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Profiles;

/// <summary>
///     Ceremonies Profile for AutoMapper
/// </summary>
public sealed class CeremonyProfile : Profile
{
    /// <summary>
    ///     Constructor for profiler
    /// </summary>
    public CeremonyProfile()
    {
        CreateMap<Ceremony, CeremonyDto>().ReverseMap();
        CreateMap<Ceremony, CeremonyDto>();
        CreateMap<CeremonyDto, Ceremony>();
    }
}
