namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{
    public enum LeaveType
    {
        Sick,
        Vacation
    }
    public class LeaveRequest
    {
        public DateTime Start_Date { get; set; }

        public DateTime End_Date { get; set; }

        public int Total_Days { get; set; }

        public bool Approval { get; set; }

        public LeaveType Leave_Type { get; set; }

        public HREmployee Requesting_Employee { get; set; }
    }
}
