using Microsoft.EntityFrameworkCore;
using WeddingService.Dal.Entities;
using WeddingService.Dal.Entities.Base;

namespace WeddingService.Dal.Contexts;

/// <summary>
///		Class which represents database context to work with it
/// </summary>
public sealed class WeddingServiceContext : DbContext
{
	/// <summary>
	///		Context class constructor
	/// </summary>
	/// <param name="options">Configuring db context</param>
	public WeddingServiceContext(DbContextOptions<WeddingServiceContext> options) : base(options)
	{
	}

	/// <summary>
	///		DbSet for base service
	/// </summary>
	public DbSet<BaseServiceEntity> BaseServices { get; set; } = null!;

	/// <summary>
	///		DbSet for cars service
	/// </summary>
	public DbSet<Cars> Cars { get; set; } = null!;

	/// <summary>
	///		DbSet for ceremonies service
	/// </summary>
	public DbSet<Ceremonies> Ceremonies { get; set; } = null!;

	/// <summary>
	///		DbSet for clothes service
	/// </summary>
	public DbSet<Clothes> Clothes { get; set; } = null!;


	/// <summary>
	///		DbSet for orders service
	/// </summary>
	public DbSet<Orders> Orders { get; set; } = null!;
}
