using System.ComponentModel.DataAnnotations;

namespace Comp7071_A2.Areas.ManageCare.Models
{
    public class Invoice
    {

        public Guid Id { get; set; }

        public required DateTime StartDate { get; set; } = DateTime.MinValue;

        public required DateTime EndDate { get; set; } = DateTime.MaxValue;

        [Required]
        public Guid CustomerId { get; set; }
        

        public Customer? Customer { get; set; } 
    
        public ICollection<InvoiceLine> Lines { get; set;} = [];
    }
}
