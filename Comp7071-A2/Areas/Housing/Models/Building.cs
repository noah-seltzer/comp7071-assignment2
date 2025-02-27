using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class Building
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("HousingGroup")]
        public Guid? HousingGroupID { get; set; }

        public int NumUnits { get; set; }
        public int NumLockers { get; set; }
        public int NumParking { get; set; }

        public virtual HousingGroup? HousingGroup { get; set; }
    }
}
