using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class Application
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [ForeignKey("Renter")]
        public string? RenterID { get; set; }  // Links to IdentityUser ID

        [Required]
        [MaxLength(10)]
        public string Status { get; set; } = "Pending";  // Default status

        public virtual IdentityUser Renter { get; set; }
        public virtual ICollection<ApplicationReference> ApplicationReferences { get; set; } = new List<ApplicationReference>();
    }
}