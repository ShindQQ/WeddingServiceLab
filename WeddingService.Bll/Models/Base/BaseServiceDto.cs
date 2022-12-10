namespace WeddingService.Bll.Models.Base;

/// <summary>
///     Dto for base service
/// </summary>
public class BaseServiceDto
{
    /// <summary>
    ///     Id of the service
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    ///     Price of the serivce
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    ///     Name of the service
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
