namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{
    public class Payroll
    {
        public Guid ID { get; set; }
        public Guid EmployeeId { get; set; }

        public ICollection<PayPeriod> PayPeriods { get; set; } = new List<PayPeriod>();
        public (float ScheduledHours, float HoursMissed) Attendance { get; set; }

        public bool isHourly { get; set; }
        public float Pay_Rate { get; set; }

        public float Tax_Deduction { get; set; }
        public float Pay_Deduction { get; set; }
        public float Overtime_Hours { get; set; }
        public float Vacation_Pay { get; set; }
        public float Total_Pay { get; set; }
    }
}
