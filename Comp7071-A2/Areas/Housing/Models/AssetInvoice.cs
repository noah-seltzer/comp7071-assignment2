using System.ComponentModel.DataAnnotations;

namespace Comp7071_A2.Areas.Housing.Models
{
        public class AssetInvoice
        {
                public Guid Id { get; set; }
                
                public required Guid RenterId { get; set; }
                
                public required Guid AssetId { get; set; }

                [DataType(DataType.Date)]
                public required DateTime StartDate { get; set; }

                [DataType(DataType.Date)]
                public required DateTime EndDate { get; set; }
                
                public Renter? Renter { get; set; }
                
                public Asset? Asset { get; set; }

                public ICollection<AssetInvoiceLine> Lines { get; set; } = [];
        }
}
