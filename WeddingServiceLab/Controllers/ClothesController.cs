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
public sealed class ClothesController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IClothesService _clothesService;

    public ClothesController(IMapper mapper, IClothesService clothesService)
    {
        _mapper = mapper;
        _clothesService = clothesService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddClothAsync(ClothesDto clothesDto)
    {
        var clothCreated = await _clothesService.AddAsync(_mapper.Map<Clothes>(clothesDto));

        return CreatedAtRoute("GetCloth",
            new { clothCreated.Id, clothCreated.Price, clothCreated.Name, clothCreated.Orders }, clothCreated);
    }

    [HttpGet]
    public async Task<IActionResult> GetClothAsync()
    {
        var clothesFromRepo = await _clothesService.GetAsync();

        return Ok(clothesFromRepo);
    }

    [HttpGet("{id}", Name = "GetCloth")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClothesAsync(long id)
    {
        var clothesFromRepo = await _clothesService.FindAsync(new ClothesDto { Id = id });

        if (clothesFromRepo == null) return NotFound();

        return Ok(clothesFromRepo);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateClothAsync([Required] long clothId, ClothesDto clothForUpdate)
    {
        if (!await _clothesService.IsExistAsync(clothForUpdate)) return NotFound();

        var updatedCloth = _mapper.Map<Clothes>(clothForUpdate);
        updatedCloth.Id = clothId;

        await _clothesService.UpdateAsync(updatedCloth);

        return Ok(updatedCloth);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteClothAsync([Required] long clothId)
    {
        var clothForDelete = new Clothes { Id = clothId };
        await _clothesService.DeleteAsync(clothForDelete);

        return Ok(clothForDelete);
    }
}
