using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WeddingService.Bll.Models;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Entities;

namespace WeddingServiceLab.Controllers;

/// <summary>
///     Controller to work with cars services
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status200OK)]
public sealed class CarsController : ControllerBase
{
    /// <summary>
    ///     Mapping Cars Entities
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    ///     Cars service
    /// </summary>
    private readonly ICarsService _carsService;

    /// <summary>
    ///     Constructor for controller
    /// </summary>
    /// <param name="mapper">Mapper configuration</param>
    /// <param name="carsService">Cars service</param>
    public CarsController(IMapper mapper, ICarsService carsService)
    {
        _mapper = mapper;
        _carsService = carsService;
    }

    /// <summary>
    ///     Adding car service to db
    /// </summary>
    /// <param name="carDto">Car which will be added</param>
    /// <returns>Added car</returns>
    /// <status code="200">Success</status>
    /// <status code="201">Car is created</status>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddCarAsync(CarsDto carDto)
    {
        var carCreated = await _carsService.AddAsync(_mapper.Map<Cars>(carDto));

        return CreatedAtRoute("GetCar",
            new { carCreated.Id, carCreated.Price, carCreated.Name, carCreated.Orders }, carCreated);
    }

    /// <summary>
    ///     Receiving list of all cars
    /// </summary>
    /// <returns>List of all cars</returns>
    /// <status code="200">Success</status>
    [HttpGet]
    public async Task<IActionResult> GetCarsAsync()
    {
        var carsFromRepo = await _carsService.GetAsync();

        return Ok(carsFromRepo);
    }

    /// <summary>
    ///     Receiving car by id
    /// </summary>
    /// <param name="id">Id of the car service</param>
    /// <returns>Car service by id</returns>
    /// <status code="200">Car found</status>
    /// <status code="404">Car service not found</status>
    [HttpGet("{id}", Name = "GetCar")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCarAsync(long id)
    {
        var carFromRepo = await _carsService.FindAsync(new CarsDto { Id = id });

        if (carFromRepo == null) return NotFound();

        return Ok(carFromRepo);
    }

    /// <summary>
    ///     Updating car by id
    /// </summary>
    /// <param name="carId">Car service id</param>
    /// <param name="carForUpdate">Data for updating car service</param>
    /// <returns>Updated car service</returns>
    /// <status code="200">Car updated</status>
    /// <status code="404">Car service not found</status>
    /// <status code="500">Happened troubles with updating car service</status>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCarAsync([Required] long carId, CarsDto carForUpdate)
    {
        if (!await _carsService.IsExistAsync(new CarsDto { Id = carId })) return NotFound();

        var updatedCar = _mapper.Map<Cars>(carForUpdate);
        updatedCar.Id = carId;

        await _carsService.UpdateAsync(updatedCar);

        return Ok(updatedCar);
    }

    /// <summary>
    ///     Removing car service by id
    /// </summary>
    /// <param name="carId">Car service id</param>
    /// <returns>Deleted car service</returns>
    /// <status code="200">Car removed</status>
    /// <status code="404">Car service not found</status>
    /// <status code="500">Happened troubles with removing car service</status>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCarAsync([Required] long carId)
    {
        if (!await _carsService.IsExistAsync(new CarsDto { Id = carId })) return NotFound();

        var carForDelete = new Cars { Id = carId };
        await _carsService.DeleteAsync(carForDelete);

        return Ok(carForDelete);
    }
}
