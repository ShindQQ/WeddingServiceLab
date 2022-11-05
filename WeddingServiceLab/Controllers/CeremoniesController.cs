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
public sealed class CeremoniesController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly ICeremoniesService _ceremoniesService;

    public CeremoniesController(IMapper mapper, ICeremoniesService ceremoniesService)
    {
        _mapper = mapper;
        _ceremoniesService = ceremoniesService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddCeremonyAsync(CeremoniesDto ceremonyDto)
    {
        var ceremonyCreated = await _ceremoniesService.AddAsync(_mapper.Map<Ceremonies>(ceremonyDto));

        return CreatedAtRoute("GetCeremony",
            new { ceremonyCreated.Id, ceremonyCreated.Price, ceremonyCreated.Name, ceremonyCreated.Orders }, ceremonyCreated);
    }

    [HttpGet]
    public async Task<IActionResult> GetCeremoniesAsync()
    {
        var ceremoniesFromRepo = await _ceremoniesService.GetAsync();

        return Ok(ceremoniesFromRepo);
    }

    [HttpGet("{id}", Name = "GetCeremony")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCeremonyAsync(long id)
    {
        var ceremonyFromRepo = await _ceremoniesService.FindAsync(new CeremoniesDto { Id = id });

        if (ceremonyFromRepo == null) return NotFound();

        return Ok(ceremonyFromRepo);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCeremonyAsync([Required] long ceremonyId, CeremoniesDto ceremonyForUpdate)
    {
        if (!await _ceremoniesService.IsExistAsync(ceremonyForUpdate)) return NotFound();

        var updatedCeremony = _mapper.Map<Ceremonies>(ceremonyForUpdate);
        updatedCeremony.Id = ceremonyId;

        await _ceremoniesService.UpdateAsync(updatedCeremony);

        return Ok(updatedCeremony);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCeremonyAsync([Required] long ceremonyId)
    {
        var ceremonyForDelete = new Ceremonies { Id = ceremonyId };
        await _ceremoniesService.DeleteAsync(ceremonyForDelete);

        return Ok(ceremonyForDelete);
    }
}
