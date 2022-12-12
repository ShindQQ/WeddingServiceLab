using ShoShoppers.Bll.Models.Error;
using System.Net;
using WeddingService.Bll.Models;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Services;

/// <summary>
///     Class for ceremonies service
/// </summary>
public sealed class CeremoniesService : BaseService<Ceremony, CeremonyDto>, ICeremoniesService
{
    /// <summary>
    ///     Constructor for ceremonies service
    /// </summary>
    /// <param name="context">Db context</param>
    public CeremoniesService(WeddingServiceContext context) : base(context)
    {
    }

    /// <summary>
    ///     Adding Ceremonies service to db
    /// </summary>
    /// <param name="entity">Entity which will be added</param>
    /// <returns>Added entity</returns>
    public override async Task<Ceremony> AddAsync(Ceremony entity)
    {
        if (await IsExistAsync(new CeremonyDto { Name = entity.Name, Price = entity.Price }))
        {
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Entity by id {entity.Id} with such data was already added.");
        }

        return await base.AddAsync(entity);
    }
}
