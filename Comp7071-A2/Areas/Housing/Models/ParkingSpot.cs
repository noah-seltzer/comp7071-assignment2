using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class ParkingSpot
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [ForeignKey("Asset")]
        public Guid AssetID { get; set; }

        [ForeignKey("Suite")]
        public Guid? SuiteID { get; set; }

        [ForeignKey("Vehicle")]
        public Guid? VehicleID { get; set; }

        public int SpotNumber { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual Suite? Suite { get; set; }
        public virtual Vehicle? Vehicle { get; set; }

        public ParkingSpot()
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