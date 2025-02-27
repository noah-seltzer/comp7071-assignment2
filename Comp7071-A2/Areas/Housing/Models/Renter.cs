using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class Renter
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("Application")]
        public Guid? ApplicationID { get; set; }

        [ForeignKey("Asset")]
        public Guid? AssetID { get; set; }

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

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public virtual Asset? Asset { get; set; }
        public virtual Application? Application { get; set; }
    }
}