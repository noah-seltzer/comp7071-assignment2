
namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{
    public class HRManager : HREmployee
    {
        public ICollection<HREmployee> ManagedEmployees { get; set; } = new List<HREmployee>();
    }
}
