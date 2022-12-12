using WeddingService.Bll.Models;
using WeddingService.Bll.Models.Base;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Services.Interfaces;

/// <summary>
///     Interface for orders service
/// </summary>
public interface IOrdersService
{
    /// <summary>
    ///     Adding order to db
    /// </summary>
    /// <param name="entity">Order which will be added</param>
    /// <returns>Added order</returns>
    Task<Order> AddAsync(Order entity);

    /// <summary>
    ///     Adding service to order
    /// </summary>
    /// <param name="orderId">Id of the order</param>
    /// <param name="baseServiceDto">Service which will be added</param>
    /// <returns>Order with added service</returns>
    Task<Order> AddServiceToOrderAsync(long orderId, BaseServiceDto baseServiceDto);

    /// <summary>
    ///     Updating order in db
    /// </summary>
    /// <param name="entity">Order which will be updated</param>
    /// <returns>Task</returns>
    Task UpdateAsync(Order entity);

    /// <summary>
    ///     Removing order from db
    /// </summary>
    /// <param name="entity">Order for delete</param>
    /// <returns>Task</returns>
    Task DeleteAsync(Order entity);

    /// <summary>
    ///     Removing service from order
    /// </summary>
    /// <param name="orderId">Id of the order</param>
    /// <param name="serviceId">Id of the service in order for delete</param>
    /// <returns>Order with removed service</returns>
    Task<Order> DeleteServiceFromOrderAsync(long orderId, long serviceId);

    /// <summary>
    ///     Receiving all orders from db
    /// </summary>
    /// <param name="orderByDescending">Ordering by descending</param>
    /// <returns>IEnumerable of Orders</returns>
    Task<IEnumerable<Order>> GetAsync(bool orderByDescending);

    /// <summary>
    ///     Searching for Order by filter
    /// </summary>
    /// <param name="entityDto">Order with needed params</param>
    /// <returns>Found order or null if it doesn`t exist</returns>
    Task<Order?> FindAsync(OrderDto entityDto);

    /// <summary>
    ///     Checking is there such order in db
    /// </summary>
    /// <param name="entityDto">Order with neeeded params</param>
    /// <returns>True or false if not found</returns>
    Task<bool> IsExistAsync(OrderDto entityDto);
}
