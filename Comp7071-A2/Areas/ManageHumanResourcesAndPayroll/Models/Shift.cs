namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{

    public enum ShiftStatus
    {
        Upcoming, Sick, Worked, Vacation
    }
    public class Shift
    {

        public Guid ID { get; set; }
        public float Hours_Worked { get; set; }
        public float Hours_Scheduled { get; set; }

        public DateTime Start_Time { get; set; }
        public DateTime End_Time { get; set; }

        public ShiftStatus Status
        {
            get; set;

        }
    }
}