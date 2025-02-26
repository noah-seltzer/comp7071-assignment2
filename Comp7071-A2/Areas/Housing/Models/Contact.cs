using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class Contact
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [ForeignKey("Renter")]
        public string? RenterID { get; set; }  // Links to IdentityUser ID

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public virtual IdentityUser Renter { get; set; }
        public virtual ICollection<ApplicationReference> ApplicationReferences { get; set; } = new List<ApplicationReference>();
    }
}
