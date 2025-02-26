

namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{
    public class Manager: Employee
    {
        public ICollection<Employee> ManagedEmployees { get; set; } = new List<Employee>();
    }
}
