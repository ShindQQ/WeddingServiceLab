using Microsoft.EntityFrameworkCore;
using WeddingService.Dal.Entities;
using WeddingService.Dal.Entities.Base;

namespace WeddingService.Dal.Contexts;

public sealed class WeddingServiceContext : DbContext
{
	public WeddingServiceContext(DbContextOptions<WeddingServiceContext> options) : base(options)
	{
	}

	public DbSet<BaseServiceEntity> BaseServices { get; set; } = null!;

	public DbSet<Cars> Cars { get; set; } = null!;

	public DbSet<Ceremonies> Ceremonies { get; set; } = null!;

	public DbSet<Clothes> Clothes { get; set; } = null!;

	public DbSet<Orders> Orders { get; set; } = null!;
}
