using WeddingService.Bll.Models;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Services;

/// <summary>
///     Class for clothes service
/// </summary>
public sealed class ClothesService : BaseService<Clothes, ClothesDto>, IClothesService
{
    /// <summary>
    ///     Constructor for clothes service
    /// </summary>
    /// <param name="context">Db context</param>
    public ClothesService(WeddingServiceContext context) : base(context)
    {
    }
}
