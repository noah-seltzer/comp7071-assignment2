using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class ParkingSpot : Asset
    {
        public int SpotNumber { get; set; }

        [ForeignKey("Suite")]
        public Guid? SuiteID { get; set; }

        [ForeignKey("Vehicle")]
        public Guid? VehicleID { get; set; }

        public virtual Suite? Suite { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
    }
}