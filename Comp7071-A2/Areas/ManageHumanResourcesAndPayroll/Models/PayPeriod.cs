using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{
    public class PayPeriod
    {
        public Guid ID {get; set;}

        public enum PayPeriodType{
            hourly, 
            salary
        }

        public DateTime Start_Date {get; set;}

        public DateTime? End_Date { get; set; }

        public float rate {get; set;}

    }
}