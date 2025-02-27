using System.ComponentModel.DataAnnotations;

namespace Comp7071_A2.Areas.ManageCare.Models
{
    public class Certification
    {
        public Guid Id { get; set; }
            
        [Required]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public ICollection<Service>? Services { get; set; } = [];
        public ICollection<Employee>? Employees { get; set; } = [];

    }
}
