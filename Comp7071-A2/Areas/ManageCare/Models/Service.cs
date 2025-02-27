using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.ManageCare.Models
{
    public class Service
    {
        public Guid Id { get; set; }

        [Required]
        [Column("Type")]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [Range(0, 1000)]
        [DataType(DataType.Currency)]
        public decimal Rate { get; set; }

        public ICollection<Schedule>? Schedule { get; set; } = [];
        
        public ICollection<Certification>? Certifications { get; set; } = [];
    }
}
