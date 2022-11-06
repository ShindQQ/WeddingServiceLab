using WeddingService.Bll.Models;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Services;

/// <summary>
///     Class for cars service
/// </summary>
public sealed class CarsService : BaseService<Cars, CarsDto>, ICarsService
{
    /// <summary>
    ///     Constructor for cars service
    /// </summary>
    /// <param name="context">Db context</param>
    public CarsService(WeddingServiceContext context) : base(context)
    {
    }
}
