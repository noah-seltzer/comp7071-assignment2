namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{
    public class LeaveRequest
    {
        public DateTime Start_Date { get; set; }

        public DateTime End_Date { get; set; }
        
        public int Total_Days { get; set; }

        public bool approval { get; set; }

        public enum leave_type
        {
            sick,
            vacation
        }
    }
}
