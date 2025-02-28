using System.ComponentModel.DataAnnotations;

namespace Comp7071_A2.Areas.ManageCare.Models
{
        public class Invoice
        {
                public Guid Id { get; set; }

                [DataType(DataType.Date)]
                public required DateTime StartDate { get; set; }

                [DataType(DataType.Date)]
                public required DateTime EndDate { get; set; }

                public required Guid CustomerId { get; set; }

                public Customer? Customer { get; set; }

                public ICollection<InvoiceLine> Lines { get; set; } = [];
        }
}
