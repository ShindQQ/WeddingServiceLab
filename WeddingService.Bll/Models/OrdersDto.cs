using WeddingService.Bll.Models.Base;

namespace WeddingService.Bll.Models;

public sealed class OrdersDto
{
    public long? Id { get; set; }

    public decimal? TotalPrice { get; set; }
}
