using WeddingService.Bll.Models;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Services;

public sealed class CeremoniesService : BaseService<Ceremonies, CeremoniesDto>, ICeremoniesService
{
    public CeremoniesService(WeddingServiceContext context) : base(context)
    {
    }
}
