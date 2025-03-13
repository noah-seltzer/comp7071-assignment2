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

        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(6)]
        public string PostalCode { get; set; } = string.Empty;

        public virtual HousingGroup? HousingGroup { get; set; }
    }
}
