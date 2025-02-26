public enum PayPeriodType
{
    hourly,
    salary
}

namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{

    public class PayPeriod
    {
        public Guid ID { get; set; }

        public DateTime Start_Date { get; set; }

        public DateTime? End_Date { get; set; }

        public float Rate { get; set; }

        public PayPeriodType Type { get; set; }

    }
}