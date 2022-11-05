using AutoMapper;
using WeddingService.Bll.Models;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Profiles;

public sealed class CarsProfile : Profile
{
    public CarsProfile()
    {
        CreateMap<Cars, CarsDto>().ReverseMap();
        CreateMap<Cars, CarsDto>();
        CreateMap<CarsDto, Cars>();
    }
}
