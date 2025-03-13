using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comp7071_A2.Areas.Housing.Models
{
    public class Locker : Asset
    {
        public int LockerNumber { get; set; }

        [MaxLength(10)]
        public string LockerSize { get; set; } = string.Empty;

        [ForeignKey("Suite")]
        public Guid? SuiteID { get; set; }
        
        public virtual Suite? Suite { get; set; }
    }
}