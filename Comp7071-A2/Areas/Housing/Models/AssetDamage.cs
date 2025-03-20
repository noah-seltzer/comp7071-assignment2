using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.Housing.Models;

public class AssetDamage
{
    [Key]
    public Guid ID { get; set; }

    [ForeignKey("Asset")]
    public Guid? AssetID { get; set; }

    [ForeignKey("Renter")]
    public Guid? RenterID { get; set; }

    public string Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime RecordedDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? FixedDate { get; set; }

    public virtual Asset? Asset { get; set; }

    public virtual Renter? Renter { get; set; }

    public virtual ICollection<DamageImage>? DamageImages { get; set; }
}