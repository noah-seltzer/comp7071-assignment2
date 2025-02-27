using System.ComponentModel.DataAnnotations;

namespace Comp7071_A2.Areas.ManageCare.Models
{
    public class InvoiceLine
    {

        public Guid Id { get; set; }

        public required Guid InvoiceId { get; set; }

        public required decimal Amount { get; set; }

        public Invoice? Invoice { get; set; }
    }
}
