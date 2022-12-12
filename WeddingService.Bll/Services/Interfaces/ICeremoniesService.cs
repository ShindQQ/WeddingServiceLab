using WeddingService.Bll.Models;
using WeddingService.Dal.Entities;

namespace WeddingService.Bll.Services.Interfaces;

/// <summary>
///     Interface for ceremonies service
/// </summary>
public interface ICeremoniesService : IBaseService<Ceremony, CeremonyDto>
{
}
