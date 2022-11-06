using WeddingService.Bll.Models;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Services;

/// <summary>
///     Class for ceremonies service
/// </summary>
public sealed class CeremoniesService : BaseService<Ceremonies, CeremoniesDto>, ICeremoniesService
{
    /// <summary>
    ///     Constructor for ceremonies service
    /// </summary>
    /// <param name="context">Db context</param>
    public CeremoniesService(WeddingServiceContext context) : base(context)
    {
    }
}
