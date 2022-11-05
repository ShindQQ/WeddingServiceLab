using Microsoft.EntityFrameworkCore;
using WeddingService.Bll.Models.Base;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
using WeddingService.Dal.Entities.Base;

namespace WeddingService.Bll.Services;

public abstract class BaseService<T1, T2> : IBaseService<T1, T2> where T1 : BaseServiceEntity where T2 : BaseServiceDto
{
	protected readonly WeddingServiceContext Context;

	protected BaseService(WeddingServiceContext context)
	{
		Context = context;
	}

	public virtual async Task<T1> AddAsync(T1 entity)
	{
		await Context.Set<T1>().AddAsync(entity);
		await Context.SaveChangesAsync();

		return entity;
	}

	public virtual async Task UpdateAsync(T1 entity)
	{
		Context.Set<T1>().Update(entity);
		await Context.SaveChangesAsync();
	}

	public virtual async Task DeleteAsync(T1 entity)
	{
		Context.Set<T1>().Remove(entity);
		await Context.SaveChangesAsync();
	}

	public virtual async Task<IEnumerable<T1>> GetAsync()
	{
		return await Context.Set<T1>().Include(e => e.Orders).ToListAsync();
	}

	public virtual async Task<T1?> FindAsync(T2 entityDto)
	{
		return await Context.Set<T1>()
			.Include(e => e.Orders)
			.Where(filter => !entityDto.Id.HasValue || filter.Id == entityDto.Id)
			.Where(filter => !entityDto.Price.HasValue || filter.Price == entityDto.Price)
			.Where(filter => string.IsNullOrEmpty(entityDto.Name) || filter.Name.ToLower().Contains(entityDto.Name.ToLower()))
			.FirstOrDefaultAsync();
	}

	public virtual async Task<bool> IsExistAsync(T2 entityDto)
	{
		return await Context.Set<T1>()
            .Include(e => e.Orders)
            .Where(filter => !entityDto.Id.HasValue || filter.Id == entityDto.Id)
			.Where(filter => !entityDto.Price.HasValue || filter.Price == entityDto.Price)
			.Where(filter => string.IsNullOrEmpty(entityDto.Name) || filter.Name.ToLower().Contains(entityDto.Name.ToLower()))
			.AnyAsync();
	}
}
