using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WeddingService.Bll.Models;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Entities;

namespace WeddingServiceLab.Controllers;

/// <summary>
///     Controller to work with clothes services
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status200OK)]
public sealed class ClothesController : ControllerBase
{
    /// <summary>
    ///     Mapping Clothes Entities
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    ///     Clothes service
    /// </summary>
    private readonly IClothesService _clothesService;

    /// <summary>
    ///     Constructor for controller
    /// </summary>
    /// <param name="mapper">Mapper configuration</param>
    /// <param name="clothesService">Clothes service</param>
    public ClothesController(IMapper mapper, IClothesService clothesService)
    {
        _mapper = mapper;
        _clothesService = clothesService;
    }

    /// <summary>
    ///     Adding cloth service to db
    /// </summary>
    /// <param name="clothesDto">Cloth which will be added</param>
    /// <returns>Added cloth</returns>
    /// <status code="200">Success</status>
    /// <status code="201">Cloth is created</status>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddClothAsync(ClothesDto clothesDto)
    {
        var clothCreated = await _clothesService.AddAsync(_mapper.Map<Clothes>(clothesDto));

        return CreatedAtRoute("GetCloth",
            new { clothCreated.Id, clothCreated.Price, clothCreated.Name, clothCreated.Orders }, clothCreated);
    }

    /// <summary>
    ///     Receiving list of all clothes
    /// </summary>
    /// <returns>List of all clothes</returns>
    /// <status code="200">Success</status>
    [HttpGet]
    public async Task<IActionResult> GetClothesAsync()
    {
        var clothesFromRepo = await _clothesService.GetAsync();

        return Ok(clothesFromRepo);
    }

    /// <summary>
    ///     Receiving cloth by id
    /// </summary>
    /// <param name="id">Id of the cloth service</param>
    /// <returns>Cloth service by id</returns>
    /// <status code="200">Cloth found</status>
    /// <status code="404">Cloth service not found</status>
    [HttpGet("{id}", Name = "GetCloth")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClothAsync([Required] long id)
    {
        var clothesFromRepo = await _clothesService.FindAsync(new ClothesDto { Id = id });

        if (clothesFromRepo == null) return NotFound();

        return Ok(clothesFromRepo);
    }

    /// <summary>
    ///     Updating cloth by id
    /// </summary>
    /// <param name="clothId">Cloth service id</param>
    /// <param name="clothForUpdate">Data for updating cloth service</param>
    /// <returns>Updated cloth service</returns>
    /// <status code="200">Cloth updated</status>
    /// <status code="404">Cloth service not found</status>
    /// <status code="500">Happened troubles with updating cloth service</status>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateClothAsync([Required] long clothId, ClothesDto clothForUpdate)
    {
        if (!await _clothesService.IsExistAsync(new ClothesDto { Id = clothId })) return NotFound();

        var updatedCloth = _mapper.Map<Clothes>(clothForUpdate);
        updatedCloth.Id = clothId;

        await _clothesService.UpdateAsync(updatedCloth);

        return Ok(updatedCloth);
    }

    /// <summary>
    ///     Removing clothes service by id
    /// </summary>
    /// <param name="clothId">Cloth service id</param>
    /// <returns>Deleted clothes service</returns>
    /// <status code="200">Cloth removed</status>
    /// <status code="404">Cloth service not found</status>
    /// <status code="500">Happened troubles with removing clothes service</status>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteClothAsync([Required] long clothId)
    {
        if (!await _clothesService.IsExistAsync(new ClothesDto { Id = clothId })) return NotFound();

        var clothForDelete = new Clothes { Id = clothId };
        await _clothesService.DeleteAsync(clothForDelete);

        return Ok(clothForDelete);
    }
}
