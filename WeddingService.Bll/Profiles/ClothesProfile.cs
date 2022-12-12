using AutoMapper;
using WeddingService.Bll.Models;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Profiles;

/// <summary>
///     Clothes Profile for AutoMapper
/// </summary>
public sealed class ClothesProfile : Profile
{
    /// <summary>
    ///     Constructor for profiler
    /// </summary>
    public ClothesProfile()
    {
        CreateMap<Cloth, ClothDto>().ReverseMap();
        CreateMap<Cloth, ClothDto>();
        CreateMap<ClothDto, Cloth>();
    }
}
