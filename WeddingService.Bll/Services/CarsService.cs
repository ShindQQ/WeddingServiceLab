using WeddingService.Bll.Models;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Services;

public sealed class CarsService : BaseService<Cars, CarsDto>, ICarsService
{
    public CarsService(WeddingServiceContext context) : base(context)
    {
    }
}
