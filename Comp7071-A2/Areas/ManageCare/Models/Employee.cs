using System.ComponentModel.DataAnnotations;

namespace Comp7071_A2.Areas.ManageCare.Models
{
    public class Employee
    {
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public string? Address { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? EmergencyContactPhone { get; set; }

        [StringLength(50)]
        public string? JobTitle { get; set; }

        public string? EmployeeType { get; set; }

        public Guid? ManagerId { get; set; }

        public ICollection<Schedule>? Schedule { get; set; } = [];

        public ICollection<Certification>? Certifications { get; set; } = [];
    }
}
