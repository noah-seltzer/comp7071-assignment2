using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class Application
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [ForeignKey("Renter")]
        public Guid RenterID { get; set; }

        [Required]
        [ForeignKey("Asset")]
        public Guid AssetID { get; set; }

        public virtual Asset? Asset { get; set; }

        [Required]
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;  // Default status

        public virtual Renter? Renter { get; set; }

        public virtual ICollection<ApplicationReference> ApplicationReferences { get; set; } = new List<ApplicationReference>();

        [Required]
        [Display(Name = "Monthly Rent")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal RentAmount { get; set; }
    }
}
