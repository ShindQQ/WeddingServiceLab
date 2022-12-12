using ShoShoppers.Bll.Models.Error;
using System.Net;
using WeddingService.Bll.Models;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Services;

/// <summary>
///     Class for clothes service
/// </summary>
public sealed class ClothesService : BaseService<Cloth, ClothDto>, IClothesService
{
    /// <summary>
    ///     Constructor for clothes service
    /// </summary>
    /// <param name="context">Db context</param>
    public ClothesService(WeddingServiceContext context) : base(context)
    {
    }

    /// <summary>
    ///     Adding Clothes service to db
    /// </summary>
    /// <param name="entity">Entity which will be added</param>
    /// <returns>Added entity</returns>
    public override async Task<Cloth> AddAsync(Cloth entity)
    {
        if (await IsExistAsync(new ClothDto { Name = entity.Name, Price = entity.Price }))
        {
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Entity by id {entity.Id} with such data was already added.");
        }

        return await base.AddAsync(entity);
    }
}
