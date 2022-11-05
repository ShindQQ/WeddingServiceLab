using WeddingService.Bll.Models.Base;
using WeddingService.Dal.Entities.Base;

namespace WeddingService.Bll.Services.Interfaces;

public interface IBaseService<T1, T2> where T1 : BaseServiceEntity where T2 : BaseServiceDto
{
    Task<T1> AddAsync(T1 entity);

    Task UpdateAsync(T1 entity);

    Task DeleteAsync(T1 entity);

    Task<IEnumerable<T1>> GetAsync();

    Task<T1?> FindAsync(T2 entityDto);

    Task<bool> IsExistAsync(T2 entityDto);
}
