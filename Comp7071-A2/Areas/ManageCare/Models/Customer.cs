using System.ComponentModel.DataAnnotations;

namespace Comp7071_A2.Areas.ManageCare.Models
{
    public class Customer
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Customer name required")]
        public required string Name { get; set; }


        public ICollection<Invoice>? Invoices { get; set; } = [];

        public ICollection<Schedule>? Schedules { get; set; } = [];
    }
}
