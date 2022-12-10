using Microsoft.EntityFrameworkCore;
using ShoShoppers.Bll.Models.Error;
using System.Net;
using WeddingService.Bll.Models;
using WeddingService.Bll.Models.Base;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
using WeddingService.Dal.Entities;
using WeddingService.Dal.Entities.Base;

namespace WeddingService.Bll.Services;

/// <summary>
///     Class for orders service
/// </summary>
public sealed class OrdersService : IOrdersService
{
    /// <summary>
    ///		Db context
    /// </summary>
    private readonly WeddingServiceContext Context;

    private readonly BaseService<BaseServiceEntity, BaseServiceDto> _baseService;

    /// <summary>
	///		Contructor for orders service
	/// </summary>
	/// <param name="context">Db context</param>
    public OrdersService(WeddingServiceContext context)
    {
        Context = context;
        _baseService = new(context);
    }

    /// <summary>
    ///     Adding order to db
    /// </summary>
    /// <param name="entity">Order which will be added</param>
    /// <returns>Added order</returns>
    public async Task<Orders> AddAsync(Orders entity)
    {
        await Context.Orders.AddAsync(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    ///     Adding service to order
    /// </summary>
    /// <param name="orderId">Id of the order</param>
    /// <param name="baseServiceDto">Service which will be added</param>
    /// <returns>Order with added service</returns>
    public async Task<Orders> AddServiceToOrderAsync(long orderId, BaseServiceDto baseServiceDto)
    {
        var order = await FindAsync(new OrdersDto { Id = orderId });

        if (order == null)
        {
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Order by id {orderId} wasn`t found.");
        }

        var baseService = await _baseService.FindAsync(baseServiceDto);

        if (baseService == null)
        {
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Base entity by id {baseServiceDto.Id} wasn`t found.");
        }
        else if(order.Services.Contains(baseService))
        {
            throw new HttpStatusCodeException(HttpStatusCode.BadRequest, $"Base entity by id {baseServiceDto.Id} already added.");
        }

        order.TotalPrice += baseService.Price;
        order.Services.Add(baseService);
        await Context.SaveChangesAsync();

        return order;
    }

    /// <summary>
    ///     Removing order from db
    /// </summary>
    /// <param name="entity">Order for delete</param>
    /// <returns>Task</returns>
    public async Task DeleteAsync(Orders entity)
    {
        Context.Orders.Remove(entity);
        await Context.SaveChangesAsync();
    }

    /// <summary>
    ///     Removing service from order
    /// </summary>
    /// <param name="orderId">Id of the order</param>
    /// <param name="serviceId">Id of the service in order for delete</param>
    /// <returns>Order with removed service</returns>
    public async Task<Orders> DeleteServiceFromOrderAsync(long orderId, long serviceId)
    {
        var order = await FindAsync(new OrdersDto { Id = orderId });

        if (order == null)
        {
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Order by id {orderId} wasn`t found.");
        }

        var baseService = await _baseService.FindAsync(new BaseServiceDto { Id = serviceId });

        if (baseService == null)
        {
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Base entity by id {serviceId} wasn`t found.");
        }
        else if(!order.Services.Contains(baseService))
        {
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Base entity by id {serviceId} wasn`t found in order by id {orderId}.");
        }

        order.TotalPrice -= baseService.Price;
        order.Services.Remove(baseService);
        await Context.SaveChangesAsync();

        return order;
    }

    /// <summary>
    ///     Searching for Order by filter
    /// </summary>
    /// <param name="entityDto">Order with needed params</param>
    /// <returns>Found order or null if it doesn`t exist</returns>
    public async Task<Orders?> FindAsync(OrdersDto entityDto)
    {
        return await Context.Orders
            .Include(e => e.Services)
            .Where(filter => !entityDto.Id.HasValue || filter.Id == entityDto.Id)
            .Where(filter => !entityDto.TotalPrice.HasValue || filter.TotalPrice == entityDto.TotalPrice)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    ///     Receiving all orders from db
    /// </summary>
    /// <param name="orderByDescending">Ordering by descending</param>
    /// <returns>IEnumerable of Orders</returns>
    public async Task<IEnumerable<Orders>> GetAsync(bool orderByDescending)
    {
        var orders = Context.Orders.AsQueryable();

        if (orderByDescending)
        {
            await orders.Include(e => e.Services.OrderByDescending(s => s.Price)).ToListAsync();
        }
        else
        {
            await orders.Include(e => e.Services).ToListAsync();
        }

        return orders;
    }

    /// <summary>
    ///     Checking is there such order in db
    /// </summary>
    /// <param name="entityDto">Order with neeeded params</param>
    /// <returns>True or false if not found</returns>
    public async Task<bool> IsExistAsync(OrdersDto entityDto)
    {
        return await Context.Orders
            .Include(e => e.Services)
            .Where(filter => !entityDto.Id.HasValue || filter.Id == entityDto.Id)
            .Where(filter => !entityDto.TotalPrice.HasValue || filter.TotalPrice == entityDto.TotalPrice)
            .AnyAsync();
    }

    /// <summary>
    ///     Updating order in db
    /// </summary>
    /// <param name="entity">Order which will be updated</param>
    /// <returns>Task</returns>
    public async Task UpdateAsync(Orders entity)
    {
        Context.Orders.Update(entity);
        await Context.SaveChangesAsync();
    }
}
