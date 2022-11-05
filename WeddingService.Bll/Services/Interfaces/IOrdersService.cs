using WeddingService.Bll.Models;
using WeddingService.Bll.Models.Base;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Services.Interfaces;

public interface IOrdersService
{
    Task<Orders> AddAsync(Orders entity);

    Task<Orders> AddServiceToOrderAsync(long orderId, ServiceDto baseServiceDto);

    Task UpdateAsync(Orders entity);

    Task DeleteAsync(Orders entity);

    Task<Orders> DeleteServiceFromOrderAsync(long orderId, long serviceId);

    Task<IEnumerable<Orders>> GetAsync(bool orderByDescending);

    Task<Orders?> FindAsync(OrdersDto entityDto);

    Task<bool> IsExistAsync(OrdersDto entityDto);
}
