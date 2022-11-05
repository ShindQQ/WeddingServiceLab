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

public sealed class OrdersService : IOrdersService
{
    private readonly WeddingServiceContext Context;

    public OrdersService(WeddingServiceContext context)
    {
        Context = context;
    }

    public async Task<Orders> AddAsync(Orders entity)
    {
        await Context.Orders.AddAsync(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    public async Task<Orders> AddServiceToOrderAsync(long orderId, ServiceDto baseServiceDto)
    {
        var order = await FindAsync(new OrdersDto { Id = orderId });

        if (order == null)
        {
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Order by id {orderId} wasn`t found.");
        }

        var baseService = await FindBaseServiceDtoAsync(baseServiceDto);

        if (baseService == null)
        {
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, $"Base entity by id {baseServiceDto.Id} wasn`t found.");
        }

        order.TotalPrice += baseService.Price;
        order.Services.Add(baseService);
        await Context.SaveChangesAsync();

        return order;
    }

    private async Task<BaseServiceEntity?> FindBaseServiceDtoAsync(ServiceDto baseServiceDto)
    {
        return await Context.BaseServices
            .Include(e => e.Orders)
            .Where(filter => !baseServiceDto.Id.HasValue || filter.Id == baseServiceDto.Id)
            .Where(filter => !baseServiceDto.Price.HasValue || filter.Price == baseServiceDto.Price)
            .Where(filter => string.IsNullOrEmpty(baseServiceDto.Name) || filter.Name.ToLower().Contains(baseServiceDto.Name.ToLower()))
            .FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(Orders entity)
    {
        Context.Orders.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task<Orders?> FindAsync(OrdersDto entityDto)
    {
        return await Context.Orders
            .Include(e => e.Services)
            .Where(filter => !entityDto.Id.HasValue || filter.Id == entityDto.Id)
            .Where(filter => !entityDto.TotalPrice.HasValue || filter.TotalPrice == entityDto.TotalPrice)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Orders>> GetAsync()
    {
        return await Context.Orders.Include(e => e.Services).ToListAsync();
    }

    public async Task<bool> IsExistAsync(OrdersDto entityDto)
    {
        return await Context.Orders
            .Include(e => e.Services)
            .Where(filter => !entityDto.Id.HasValue || filter.Id == entityDto.Id)
            .Where(filter => !entityDto.TotalPrice.HasValue || filter.TotalPrice == entityDto.TotalPrice)
            .AnyAsync();
    }

    public async Task UpdateAsync(Orders entity)
    {
        Context.Orders.Update(entity);
        await Context.SaveChangesAsync();
    }
}
