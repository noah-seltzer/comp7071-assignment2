using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{
    public class Schedule
    {
        public Guid ID { get; set; }

        public DateTime Start_Date { get; set; }

        public DateTime? End_Date { get; set; }

        public float Hours_Scheduled { get; set; }

        public enum Status
        {
            Scheduled,
            Cancelled
        }

        public enum Recurrance
        {
            Once,
            Daily,
            Weekly,
            Monthly
        }

        public ICollection<Shift> Shifts { get; set; } = new List<Shift>();


    }
}