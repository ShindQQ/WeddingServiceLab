namespace WeddingService.Bll.Models;

/// <summary>
///     Orders Dto
/// </summary>
public sealed class OrdesDto
{
    /// <summary>
    ///     Id of the order
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    ///     Total price of the order depending on the services
    /// </summary>
    public decimal? TotalPrice { get; set; }
}
