using WeddingService.Bll.Models.Base;
using WeddingService.Dal.Entities.Base;

namespace WeddingService.Bll.Services.Interfaces;

/// <summary>
///     Interface for base service
/// </summary>
/// <typeparam name="T1">Represents entity</typeparam>
/// <typeparam name="T2">Represents dto</typeparam>
public interface IBaseService<T1, T2> where T1 : BaseServiceEntity where T2 : BaseServiceDto
{
    /// <summary>
    ///     Adding service to db
    /// </summary>
    /// <param name="entity">Entity which will be added</param>
    /// <returns>Added entity</returns>
    Task<T1> AddAsync(T1 entity);

    /// <summary>
    ///     Updating service in db
    /// </summary>
    /// <param name="entity">Entity fo update</param>
    /// <returns>Task</returns>
    Task UpdateAsync(T1 entity);

    /// <summary>
    ///     Removing entity from db
    /// </summary>
    /// <param name="entity">Entity for delete</param>
    /// <returns>Task</returns>
    Task DeleteAsync(T1 entity);

    /// <summary>
    ///     Receiving services from db
    /// </summary>
    /// <returns>IEnumerable of entity</returns>
    Task<IEnumerable<T1>> GetAsync();

    /// <summary>
    ///     Searching for entity by filter
    /// </summary>
    /// <param name="entityDto">Dto with needed params</param>
    /// <returns>Entity or null if not found</returns>
    Task<T1?> FindAsync(T2 entityDto);

    /// <summary>
    ///     Checking existing of the entity by filter
    /// </summary>
    /// <param name="entityDto">Dto with needed params</param>
    /// <returns>True or false if entity not found</returns>
    ValueTask<bool> IsExistAsync(T2 entityDto);
}
