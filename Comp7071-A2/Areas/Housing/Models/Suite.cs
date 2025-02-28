using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class Suite
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("Locker")]
        public Guid? LockerID { get; set; }

        [ForeignKey("ParkingSpot")]
        public Guid? ParkingSpotID { get; set; }

        [Required]
        [ForeignKey("Asset")]
        public Guid AssetID { get; set; }

        public int UnitNumber { get; set; }
        public int Floor { get; set; }
        public int Occupants { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }

        public virtual Locker? Locker { get; set; }
        public virtual ParkingSpot? ParkingSpot { get; set; }
        public virtual Asset Asset { get; set; }

        // Constructor to ensure Asset creation
        public Suite()
        {
            Asset = new Asset
            {
                ID = Guid.NewGuid(),
                IsAvailable = true,
                RentAmount = 0
            };
            AssetID = Asset.ID;
        }
    }
}
