using System.ComponentModel.DataAnnotations;

namespace Comp7071_A2.Areas.ManageCare.Models
{
    public class Employee
    {

        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Address { get; set; }

        public required string Email { get; set; }

        public required string EmergencyContactPhone { get; set; }

        public required string JobTitle { get; set; }

        public ICollection<Schedule> Schedule { get; set; } = [];

        public ICollection<Certification> Certifications { get; set; } = [];
    }
}
