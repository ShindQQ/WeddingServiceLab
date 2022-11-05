using AutoMapper;
using WeddingService.Bll.Models;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Profiles;

public sealed class ClothesProfile : Profile
{
    public ClothesProfile()
    {
        CreateMap<Clothes, ClothesDto>().ReverseMap();
        CreateMap<Clothes, ClothesDto>();
        CreateMap<ClothesDto, Clothes>();
    }
}
