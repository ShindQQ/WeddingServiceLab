using AutoMapper;
using WeddingService.Bll.Models;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Profiles;

/// <summary>
///     Clothes Profile for AutoMapper
/// </summary>
public sealed class ClothProfile : Profile
{
    /// <summary>
    ///     Constructor for profiler
    /// </summary>
    public ClothProfile()
    {
        CreateMap<Cloth, ClothDto>().ReverseMap();
        CreateMap<Cloth, ClothDto>();
        CreateMap<ClothDto, Cloth>();
    }
}
