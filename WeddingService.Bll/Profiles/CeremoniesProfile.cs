using AutoMapper;
using WeddingService.Bll.Models;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Profiles;

public sealed class CeremoniesProfile : Profile
{
    public CeremoniesProfile()
    {
        CreateMap<Ceremonies, CeremoniesDto>().ReverseMap();
        CreateMap<Ceremonies, CeremoniesDto>();
        CreateMap<CeremoniesDto, Ceremonies>();
    }
}
