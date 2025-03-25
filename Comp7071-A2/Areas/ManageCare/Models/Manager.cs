using System.ComponentModel.DataAnnotations;

namespace Comp7071_A2.Areas.ManageCare.Models
{
    public class Manager : Employee
    {
        public string? Department { get; set; }

        public ICollection<Employee> Employees { get; set; } = [];
    }
}
