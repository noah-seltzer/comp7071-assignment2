using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class Application
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("Renter")]
        public Guid? RenterID { get; set; } 

        [Required]
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;  // Default status

        public virtual Renter? Renter { get; set; }
        public virtual ICollection<ApplicationReference> ApplicationReferences { get; set; } = new List<ApplicationReference>();
    }
}