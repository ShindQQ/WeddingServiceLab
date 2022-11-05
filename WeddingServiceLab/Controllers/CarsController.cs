using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WeddingService.Bll.Models;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Entities;

namespace WeddingServiceLab.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status200OK)]
public sealed class CarsController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly ICarsService _carsService;

    public CarsController(IMapper mapper, ICarsService carsService)
    {
        _mapper = mapper;
        _carsService = carsService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddCarAsync(CarsDto carDto)
    {
        var carCreated = await _carsService.AddAsync(_mapper.Map<Cars>(carDto));

        return CreatedAtRoute("GetCar",
            new { carCreated.Id, carCreated.Price, carCreated.Name, carCreated.Orders }, carCreated);
    }

    [HttpGet]
    public async Task<IActionResult> GetCarsAsync()
    {
        var carsFromRepo = await _carsService.GetAsync();

        return Ok(carsFromRepo);
    }

    [HttpGet("{id}", Name = "GetCar")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCarAsync(long id)
    {
        var carFromRepo = await _carsService.FindAsync(new CarsDto { Id = id });

        if (carFromRepo == null) return NotFound();

        return Ok(carFromRepo);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCarAsync([Required] long carId, CarsDto carForUpdate)
    {
        if (!await _carsService.IsExistAsync(carForUpdate)) return NotFound();

        var updatedCar = _mapper.Map<Cars>(carForUpdate);
        updatedCar.Id = carId;

        await _carsService.UpdateAsync(updatedCar);

        return Ok(updatedCar);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCarAsync([Required] long carId)
    {
        var carForDelete = new Cars { Id = carId };
        await _carsService.DeleteAsync(carForDelete);

        return Ok(carForDelete);
    }
}
