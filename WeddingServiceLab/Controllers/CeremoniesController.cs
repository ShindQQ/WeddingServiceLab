using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WeddingService.Bll.Models;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Entities;

namespace WeddingServiceLab.Controllers;

/// <summary>
///     Controller to work with ceremonies services
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status200OK)]
public sealed class CeremoniesController : ControllerBase
{
    /// <summary>
    ///     Mapping Ceremonies Entities
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    ///     Ceremonies service
    /// </summary>
    private readonly ICeremoniesService _ceremoniesService;

    /// <summary>
    ///     Constructor for controller
    /// </summary>
    /// <param name="mapper">Mapper configuration</param>
    /// <param name="ceremoniesService">Ceremonies service</param>
    public CeremoniesController(IMapper mapper, ICeremoniesService ceremoniesService)
    {
        _mapper = mapper;
        _ceremoniesService = ceremoniesService;
    }

    /// <summary>
    ///     Adding ceremony service to db
    /// </summary>
    /// <param name="ceremonyDto">Ceremony which will be added</param>
    /// <returns>Added ceremony</returns>
    /// <status code="200">Success</status>
    /// <status code="201">Ceremony is created</status>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddCeremonyAsync(CeremoniesDto ceremonyDto)
    {
        var ceremonyCreated = await _ceremoniesService.AddAsync(_mapper.Map<Ceremonies>(ceremonyDto));

        return CreatedAtRoute("GetCeremony",
            new { ceremonyCreated.Id, ceremonyCreated.Price, ceremonyCreated.Name, ceremonyCreated.Orders }, ceremonyCreated);
    }

    /// <summary>
    ///     Receiving list of all ceremonies
    /// </summary>
    /// <returns>List of all ceremonies</returns>
    /// <status code="200">Success</status>
    [HttpGet]
    public async Task<IActionResult> GetCeremoniesAsync()
    {
        var ceremoniesFromRepo = await _ceremoniesService.GetAsync();

        return Ok(ceremoniesFromRepo);
    }

    /// <summary>
    ///     Receiving ceremony by id
    /// </summary>
    /// <param name="id">Id of the ceremony service</param>
    /// <returns>Ceremony service by id</returns>
    /// <status code="200">Ceremony found</status>
    /// <status code="404">Ceremony service not found</status>
    [HttpGet("{id}", Name = "GetCeremony")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCeremonyAsync(long id)
    {
        var ceremonyFromRepo = await _ceremoniesService.FindAsync(new CeremoniesDto { Id = id });

        if (ceremonyFromRepo == null) return NotFound();

        return Ok(ceremonyFromRepo);
    }

    /// <summary>
    ///     Updating ceremony by id
    /// </summary>
    /// <param name="ceremonyId">Ceremony service id</param>
    /// <param name="ceremonyForUpdate">Data for updating ceremony service</param>
    /// <returns>Updated ceremony service</returns>
    /// <status code="200">Ceremony updated</status>
    /// <status code="404">Ceremony service not found</status>
    /// <status code="500">Happened troubles with updating ceremony service</status>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCeremonyAsync([Required] long ceremonyId, CeremoniesDto ceremonyForUpdate)
    {
        if (!await _ceremoniesService.IsExistAsync(new CeremoniesDto { Id = ceremonyId })) return NotFound();

        var updatedCeremony = _mapper.Map<Ceremonies>(ceremonyForUpdate);
        updatedCeremony.Id = ceremonyId;

        await _ceremoniesService.UpdateAsync(updatedCeremony);

        return Ok(updatedCeremony);
    }

    /// <summary>
    ///     Removing ceremonies service by id
    /// </summary>
    /// <param name="ceremonyId">Ceremony service id</param>
    /// <returns>Deleted ceremonies service</returns>
    /// <status code="200">Ceremony removed</status>
    /// <status code="404">Ceremony service not found</status>
    /// <status code="500">Happened troubles with removing ceremonies service</status>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCeremonyAsync([Required] long ceremonyId)
    {
        if (!await _ceremoniesService.IsExistAsync(new CeremoniesDto { Id = ceremonyId })) return NotFound();

        var ceremonyForDelete = new Ceremonies { Id = ceremonyId };
        await _ceremoniesService.DeleteAsync(ceremonyForDelete);

        return Ok(ceremonyForDelete);
    }
}
