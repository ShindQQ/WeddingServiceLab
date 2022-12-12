using ShoShoppers.Bll.Models.Error;
using System.Net;
using WeddingService.Bll.Models;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Services;

/// <summary>
///     Class for cars service
/// </summary>
public sealed class CarsService : BaseService<Car, CarDto>, ICarsService
{
    /// <summary>
    ///     Constructor for cars service
    /// </summary>
    /// <param name="context">Db context</param>
    public CarsService(WeddingServiceContext context) : base(context)
    {
    }

    /// <summary>
	///     Adding Cars service to db
	/// </summary>
	/// <param name="entity">Entity which will be added</param>
	/// <returns>Added entity</returns>
	public override async Task<Car> AddAsync(Car entity)
    {
        if (await IsExistAsync(new CarDto { Name = entity.Name, Price = entity.Price }))
        {
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Entity by id {entity.Id} with such data was already added.");
        }

        return await base.AddAsync(entity);
    }
}
