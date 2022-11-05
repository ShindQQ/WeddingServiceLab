using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WeddingService.Bll.Models;
using WeddingService.Bll.Models.Base;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Entities;
using WeddingService.Dal.Entities.Base;

namespace WeddingServiceLab.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status200OK)]
public sealed class OrdersController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IOrdersService _ordersService;

    public OrdersController(IMapper mapper, IOrdersService ordersService)
    {
        _mapper = mapper;
        _ordersService = ordersService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddOrderAsync(OrdersDto orderDto)
    {
        var orderCreated = await _ordersService.AddAsync(_mapper.Map<Orders>(orderDto));

        return CreatedAtRoute("GetOrder",
            new { orderCreated.Id, orderCreated.TotalPrice, orderCreated.Services }, orderCreated);
    }

    [HttpPost("AddServiceToOrder")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddServiceToOrderAsync([Required] long orderId, ServiceDto serviceEntity)
    {
        var orderCreated = await _ordersService.AddServiceToOrderAsync(orderId, serviceEntity);

        return CreatedAtRoute("GetOrder",
            new { orderCreated.Id, orderCreated.TotalPrice, orderCreated.Services }, orderCreated);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync()
    {
        var ordersFromRepo = await _ordersService.GetAsync();

        return Ok(ordersFromRepo);
    }

    [HttpGet("{id}", Name = "GetOrder")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderAsync(long id)
    {
        var orderFromRepo = await _ordersService.FindAsync(new OrdersDto { Id = id });

        if (orderFromRepo == null) return NotFound();

        return Ok(orderFromRepo);
    }

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

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteOrderAsync([Required] long orderId)
    {
        var orderForDelete = new Orders { Id = orderId };
        await _ordersService.DeleteAsync(orderForDelete);

        return Ok(orderForDelete);
    }
}
