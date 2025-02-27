using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{
    public enum ScheduleStatus
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
    public class HRSchedule
    {
        public Guid ID { get; set; }

        public DateTime Start_Date { get; set; }

        public DateTime? End_Date { get; set; }

        public float Hours_Scheduled { get; set; }

        public ScheduleStatus Status { get; set; }

        public Recurrance Recurrance { get; set; }

        public ICollection<Shift> Shifts { get; set; } = new List<Shift>();


    }
}