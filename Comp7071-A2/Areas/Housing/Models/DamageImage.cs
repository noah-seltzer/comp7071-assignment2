using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.Housing.Models;

public class DamageImage
{
    [Key]
    public Guid ID { get; set; }
    
    [ForeignKey("AssetDamage")]
    public Guid? AssetDamageID { get; set; }
    
    public byte[]? Photo  { get; set; }
    
    public virtual AssetDamage AssetDamage { get; set; }
}