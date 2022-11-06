using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WeddingService.Bll.Models;
using WeddingService.Bll.Models.Base;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Entities;

namespace WeddingServiceLab.Controllers;

/// <summary>
///     Controller to work with orders services
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status200OK)]
public sealed class OrdersController : ControllerBase
{
    /// <summary>
    ///     Mapping Orders Entities
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    ///     Orders service
    /// </summary>
    private readonly IOrdersService _ordersService;

    /// <summary>
    ///     Constructor for controller
    /// </summary>
    /// <param name="mapper">Mapper configuration</param>
    /// <param name="ordersService">Orders service</param>
    public OrdersController(IMapper mapper, IOrdersService ordersService)
    {
        _mapper = mapper;
        _ordersService = ordersService;
    }

    /// <summary>
    ///     Adding order service to db
    /// </summary>
    /// <param name="orderDto">Order which will be added</param>
    /// <returns>Added order</returns>
    /// <status code="200">Success</status>
    /// <status code="201">Order is created</status>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddOrderAsync(OrdersDto orderDto)
    {
        var orderCreated = await _ordersService.AddAsync(_mapper.Map<Orders>(orderDto));

        return CreatedAtRoute("GetOrder",
            new { orderCreated.Id, orderCreated.TotalPrice, orderCreated.Services }, orderCreated);
    }

    /// <summary>
    ///     Adding service to order
    /// </summary>
    /// <param name="orderId">Id pf the order</param>
    /// <param name="serviceEntity">Service which will be added to order</param>
    /// <returns>Added service to order</returns>
    /// <status code="200">Success</status>
    /// <status code="201">Order is created</status>
    /// <status code="404">Such order or service wasn`t found</status>
    [HttpPost("AddServiceToOrder")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddServiceToOrderAsync([Required] long orderId, ServiceDto serviceEntity)
    {
        var orderCreated = await _ordersService.AddServiceToOrderAsync(orderId, serviceEntity);

        return CreatedAtRoute("GetOrder",
            new { orderCreated.Id, orderCreated.TotalPrice, orderCreated.Services }, orderCreated);
    }

    /// <summary>
    ///     Receiving list of orders
    /// </summary>
    /// <param name="orderByDescending">Sorting by descending option, default false</param>
    /// <returns>List of orders</returns>
    /// <status code="200">Success</status>
    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync(bool orderByDescending)
    {
        var ordersFromRepo = await _ordersService.GetAsync(orderByDescending);

        return Ok(ordersFromRepo);
    }

    /// <summary>
    ///     Receiving order by id
    /// </summary>
    /// <param name="id">Id of the order</param>
    /// <returns>Order by id</returns>
    /// <status code="200">Success</status>
    /// <status code="404">Such order doesn`t found</status>
    [HttpGet("{id}", Name = "GetOrder")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderAsync(long id)
    {
        var orderFromRepo = await _ordersService.FindAsync(new OrdersDto { Id = id });

        if (orderFromRepo == null) return NotFound();

        return Ok(orderFromRepo);
    }

    /// <summary>
    ///     Updating order by id
    /// </summary>
    /// <param name="orderId">Id of the order</param>
    /// <param name="orderForUpdate">Params of the order for update</param>
    /// <returns>Updated order</returns>
    /// <status code="200">Success</status>
    /// <status code="404">Such order doesn`t found</status>
    /// <status code="500">Some troubles happened while updating</status>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateOrderAsync([Required] long orderId, OrdersDto orderForUpdate)
    {
        if (!await _ordersService.IsExistAsync(orderForUpdate)) return NotFound();

        var updatedOrder = _mapper.Map<Orders>(orderForUpdate);
        updatedOrder.Id = orderId;

        await _ordersService.UpdateAsync(updatedOrder);

        return Ok(updatedOrder);
    }

    /// <summary>
    ///     Removing order by id
    /// </summary>
    /// <param name="orderId">Id of the order</param>
    /// <returns>Removed order</returns>
    /// <status code="200">Success</status>
    /// <status code="404">Such order doesn`t found</status>
    /// <status code="500">Some troubles happened while removing</status>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteOrderAsync([Required] long orderId)
    {
        if (!await _ordersService.IsExistAsync(new OrdersDto { Id = orderId })) return NotFound();

        var orderForDelete = new Orders { Id = orderId };
        await _ordersService.DeleteAsync(orderForDelete);

        return Ok(orderForDelete);
    }

    /// <summary>
    ///     Removing service by id from the order by id
    /// </summary>
    /// <param name="orderId">Id of the order</param>
    /// <param name="serviceId">Id of the service</param>
    /// <returns>No content</returns>
    /// <status code="200">Success</status>
    /// <status code="404">Such order or service doesn`t found</status>
    /// <status code="500">Some troubles happened while removing</status>
    [HttpDelete("DeleteServiceFromOrder")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteServiceFromOrderAsync([Required] long orderId, [Required] long serviceId)
    {
        await _ordersService.DeleteServiceFromOrderAsync(orderId, serviceId);

        return Ok();
    }
}
