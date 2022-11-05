using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingService.Dal.Entities.Base;

[Table("BaseService")]
public abstract class BaseServiceEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public List<Orders> Orders { get; set; } = new();
}
