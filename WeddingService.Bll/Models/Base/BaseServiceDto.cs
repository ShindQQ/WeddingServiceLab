namespace WeddingService.Bll.Models.Base;

public abstract class BaseServiceDto
{
    public long? Id { get; set; }

    public decimal? Price { get; set; }

    public string Name { get; set; } = string.Empty;
}
