using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripMitraHolidays.Core.Models
{
    [Table("PackageInclusions")]
    public class PackageInclusion
    {
        [Key]
        public int InclusionId { get; set; }

        public int PackageId { get; set; }

        [Required, MaxLength(500)]
        public string Item { get; set; }

        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }
    }
}
