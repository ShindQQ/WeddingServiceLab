using WeddingService.Bll.Models;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Services;

public sealed class ClothesService : BaseService<Clothes, ClothesDto>, IClothesService
{
    public ClothesService(WeddingServiceContext context) : base(context)
    {
    }
}
