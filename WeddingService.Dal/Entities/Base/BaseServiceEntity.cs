using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingService.Dal.Entities.Base;

/// <summary>
///     Abstract class which will be inherited for future services
/// </summary>
[Table("BaseService")]
public abstract class BaseServiceEntity
{
    /// <summary>
    ///     Id of the service
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    ///     Price of the service
    /// </summary>
    [Required]
    public decimal Price { get; set; }

    /// <summary>
    ///     Name of the service
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Orders in which this service exists
    /// </summary>
    public List<Orders> Orders { get; set; } = new();
}
