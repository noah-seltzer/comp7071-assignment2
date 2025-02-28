using System.ComponentModel.DataAnnotations;

namespace Comp7071_A2.Areas.ManageCare.Models
{
    public class Schedule
    {
        public Guid Id { get; set; }

        public required DateTime StartTime { get; set; } = DateTime.MinValue;

        public required DateTime EndTime { get; set; } = DateTime.MaxValue;

        public required Guid ServiceId { get; set; }

        public ICollection<Employee> Employees { get; set; } = [];

        public ICollection<Customer> Customers { get; set; } = [];

        public Service? Service { get; set; }
    }
}
