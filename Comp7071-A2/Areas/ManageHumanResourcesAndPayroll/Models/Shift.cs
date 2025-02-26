namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{
    public class Shift
    {
        public float Hours_Worked { get; set; }
       
        public DateTime Start_Time { get; set; }

        public enum Status
        {
            upcoming, sick, worked, vacation
        }

    }
}
