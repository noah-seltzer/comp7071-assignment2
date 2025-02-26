namespace Comp7071_A2.Areas.ManageCare.Models
{
    public class Service
    {

        public Guid Id { get; set; }

        public required string Type { get; set; }

        public ICollection<Schedule> Schedule { get; set; } = [];
        
        public ICollection<Certification> Certifications { get; set; } = [];
    }
}
