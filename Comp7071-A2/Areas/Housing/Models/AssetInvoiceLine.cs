using System.ComponentModel.DataAnnotations;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class AssetInvoiceLine
    {
        public Guid Id { get; set; }

        public required Guid InvoiceId { get; set; }

        public required string Description { get; set; }

        public required decimal Quantity { get; set; }

        public required decimal UnitPrice { get; set; }

        public required decimal Amount { get; set; }

        public AssetInvoice? Invoice { get; set; }
    }
}
