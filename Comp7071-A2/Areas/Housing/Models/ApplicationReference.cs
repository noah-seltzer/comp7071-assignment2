using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class ApplicationReference
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [ForeignKey("Application")]
        public Guid ApplicationID { get; set; }

        [Required]
        [ForeignKey("Contact")]
        public Guid ContactID { get; set; }

        public virtual Application Application { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
