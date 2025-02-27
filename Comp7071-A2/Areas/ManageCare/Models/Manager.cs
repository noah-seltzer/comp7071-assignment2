using System.ComponentModel.DataAnnotations;

namespace Comp7071_A2.Areas.ManageCare.Models
{
    // No longer inherits from Employee
    public class Manager
    {
        public Guid Id { get; set; }
        
        [Required]
        public required string Name { get; set; }
        
        public string? Department { get; set; }
        
        public ICollection<Employee> Employees { get; set; } = [];
    }
}
