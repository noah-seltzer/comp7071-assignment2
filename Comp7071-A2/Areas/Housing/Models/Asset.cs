using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class Asset
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("HousingGroup")]
        public Guid? HousingGroupID { get; set; }

        [ForeignKey("Renter")]
        public Guid? RenterID { get; set; }

        [ForeignKey("Building")]
        public Guid? BuildingID { get; set; }

        public bool IsAvailable { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal RentAmount { get; set; }

        public virtual HousingGroup? HousingGroup { get; set; }
        public virtual Renter? Renter { get; set; }
        public virtual Building? Building { get; set; }
    }
}