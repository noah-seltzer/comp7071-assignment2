namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{
    public enum LeaveType
    {
        sick,
        vacation
    }
    public class LeaveRequest
    {
        public DateTime Start_Date { get; set; }

        public DateTime End_Date { get; set; }
        
        public int Total_Days { get; set; }

        public bool Approval { get; set; }

        public LeaveType Leave_Type { get; set; }
    }
}
