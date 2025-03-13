using Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models
{
    public class HRSchedule
    {
        public Guid ID { get; set; }

        [Display(Name = "Start Date")]
        public DateTime Start_Date { get; set; }


        [Display(Name = "End Date")]
        public DateTime End_Date { get; set; }

        [Display(Name = "Start Time")]
        public TimeOnly Start_Time { get; set; }

        [Display(Name = "Hours Per Shift")]
        [DefaultValue(8)]
        public float Hours_Scheduled { get; set; } = 8;

        [Display(Name = "Schedule Name")]
        //[Required]
        public string? Name { get; set; }

        public ScheduleStatus Status { get; set; } = ScheduleStatus.Active;

        public Recurrence Recurrance { get; set; } = Recurrence.Daily;

        public ICollection<Shift> Shifts { get; set; } = new List<Shift>();

        public void Deconstruct(out DateTime StartDate, out DateTime EndDate)
        {
            StartDate = Start_Date;
            EndDate = End_Date;
        }
    }
}