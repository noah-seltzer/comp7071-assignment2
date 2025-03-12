using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class Suite : Asset
    {
        [ForeignKey("Locker")]
        public Guid? LockerID { get; set; }

        [ForeignKey("ParkingSpot")]
        public Guid? ParkingSpotID { get; set; }

        public int UnitNumber { get; set; }
        public int Floor { get; set; }
        public int Occupants { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }

        public virtual Locker? Locker { get; set; }
        public virtual ParkingSpot? ParkingSpot { get; set; }
    }
}
