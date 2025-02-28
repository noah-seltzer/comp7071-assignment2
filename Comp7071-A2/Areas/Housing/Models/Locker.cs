using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class Locker
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [ForeignKey("Asset")]
        public Guid AssetID { get; set; }

        [ForeignKey("Suite")]
        public Guid? SuiteID { get; set; }

        public int LockerNumber { get; set; }

        [MaxLength(10)]
        public string LockerSize { get; set; } = string.Empty;


        public virtual Asset Asset { get; set; }

        public virtual Suite? Suite { get; set; }

        public Locker()
        {
            Asset = new Asset
            {
                ID = Guid.NewGuid(),
                IsAvailable = true
            };
            AssetID = Asset.ID;
        }
    }
}