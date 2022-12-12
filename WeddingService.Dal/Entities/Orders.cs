using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeddingService.Dal.Entities.Base;

namespace WeddingService.Dal.Entities;

/// <summary>
///     Orders table
/// </summary>
[Table("Orders")]
public sealed class Order
{
    /// <summary>
    ///     Id of the order
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    ///     Total price depending on price of services
    /// </summary>
    [Required]
    public decimal TotalPrice { get; set; } = 0;

    /// <summary>
    ///     Services in the order
    /// </summary>
    public List<BaseServiceEntity> Services { get; set; } = new();
}
