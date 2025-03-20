using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{
    public class HREmployee
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Adderess { get; set; }

        public string Emergency_Contact { get; set; }

        public string Job_Title { get; set; }

        public string? UserId { get; set; }

        public string Employment_Type { get; set; }

        public ICollection<PayPeriod> Pay_History { get; set; } = new List<PayPeriod>();
    }
}