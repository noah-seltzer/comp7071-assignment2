using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{
    public class Employee
    {
        public Guid ID {get; set;}

        public string Name {get; set;}
    }
}