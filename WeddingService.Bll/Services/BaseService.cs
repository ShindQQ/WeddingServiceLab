using Microsoft.EntityFrameworkCore;
using WeddingService.Bll.Models.Base;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
using WeddingService.Dal.Entities.Base;

namespace WeddingService.Bll.Services;

/// <summary>
///     Abstract class for base service
/// </summary>
/// <typeparam name="T1">Represents entity</typeparam>
/// <typeparam name="T2">Represents dto</typeparam>
public abstract class BaseService<T1, T2> : IBaseService<T1, T2> where T1 : BaseServiceEntity where T2 : BaseServiceDto
{
	/// <summary>
	///		Db context
	/// </summary>
	protected readonly WeddingServiceContext Context;

	/// <summary>
	///		Contructor for base service
	/// </summary>
	/// <param name="context">Db context</param>
	protected BaseService(WeddingServiceContext context)
	{
		Context = context;
	}

	/// <summary>
	///     Adding service to db
	/// </summary>
	/// <param name="entity">Entity which will be added</param>
	/// <returns>Added entity</returns>
	public virtual async Task<T1> AddAsync(T1 entity)
	{
		await Context.Set<T1>().AddAsync(entity);
		await Context.SaveChangesAsync();

		return entity;
	}

	/// <summary>
	///     Updating service in db
	/// </summary>
	/// <param name="entity">Entity fo update</param>
	/// <returns>Task</returns>
	public virtual async Task UpdateAsync(T1 entity)
	{
		Context.Set<T1>().Update(entity);
		await Context.SaveChangesAsync();
	}

	/// <summary>
	///     Removing entity from db
	/// </summary>
	/// <param name="entity">Entity for delete</param>
	/// <returns>Task</returns>
	public virtual async Task DeleteAsync(T1 entity)
	{
		Context.Set<T1>().Remove(entity);
		await Context.SaveChangesAsync();
	}

	/// <summary>
	///     Receiving services from db
	/// </summary>
	/// <returns>IEnumerable of entity</returns>
	public virtual async Task<IEnumerable<T1>> GetAsync()
	{
		return await Context.Set<T1>().Include(e => e.Orders).ToListAsync();
	}

	/// <summary>
	///     Searching for entity by filter
	/// </summary>
	/// <param name="entityDto">Dto with needed params</param>
	/// <returns>Entity or null if not found</returns>
	public virtual async Task<T1?> FindAsync(T2 entityDto)
	{
		return await Context.Set<T1>()
			.Include(e => e.Orders)
			.Where(filter => !entityDto.Id.HasValue || filter.Id == entityDto.Id)
			.Where(filter => !entityDto.Price.HasValue || filter.Price == entityDto.Price)
			.Where(filter => string.IsNullOrEmpty(entityDto.Name) 
			|| filter.Name.ToLower().Contains(entityDto.Name.ToLower()))
			.FirstOrDefaultAsync();
	}

	/// <summary>
	///     Checking existing of the entity by filter
	/// </summary>
	/// <param name="entityDto">Dto with needed params</param>
	/// <returns>True or false if entity not found</returns>
	public virtual async Task<bool> IsExistAsync(T2 entityDto)
	{
		return await Context.Set<T1>()
			.Include(e => e.Orders)
			.Where(filter => !entityDto.Id.HasValue || filter.Id == entityDto.Id)
			.Where(filter => !entityDto.Price.HasValue || filter.Price == entityDto.Price)
			.Where(filter => string.IsNullOrEmpty(entityDto.Name) 
			|| filter.Name.ToLower().Contains(entityDto.Name.ToLower()))
			.AnyAsync();
	}
}
