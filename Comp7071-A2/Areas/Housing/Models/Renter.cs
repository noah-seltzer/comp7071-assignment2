using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class Renter
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string? IdentityID { get; set; }

        public virtual IdentityUser? Identity { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public byte[]? Photo { get; set; }

        [Required]
        [MaxLength(100)]
        [Phone]
        public string PhoneNumber { get; set; }

        public virtual ICollection<AssetDamage>? AssetDamages { get; set; }
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
        // This is in the IdentityUser
        // [Required]
        // [MaxLength(100)]
        // [EmailAddress]
        // public string Email { get; set; }
    }
}