using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class HousingGroup
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [ForeignKey("Manager")]
        public string? ManagerID { get; set; }

        public virtual IdentityUser? Manager { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(6)]
        public string PostalCode { get; set; } = string.Empty;
    }
}