using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class Vehicle
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("ParkingSpot")]
        public Guid? ParkingSpotID { get; set; }

        [ForeignKey("Renter")]
        public Guid RenterID { get; set; }

        [MaxLength(20)]
        public string VehicleType { get; set; } = string.Empty;

        [MaxLength(10)]
        public string LicensePlate { get; set; } = string.Empty;

        public virtual ParkingSpot? ParkingSpot { get; set; }
        public virtual Renter? Renter { get; set; }
    }
}