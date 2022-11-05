using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeddingService.Dal.Entities.Base;

namespace WeddingService.Dal.Entities;

[Table("Orders")]
public sealed class Orders
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public decimal TotalPrice { get; set; } = 0;

    public List<BaseServiceEntity> Services { get; set; } = new();
}
