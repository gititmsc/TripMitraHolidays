using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripMitraHolidays.Core.Models
{
    [Table("PackageImages")]
    public class PackageImage
    {
        [Key]
        public int ImageId { get; set; }

        public int PackageId { get; set; }

        [Required, MaxLength(500)]
        public string ImagePath { get; set; }

        public int DisplayOrder { get; set; }

        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }
    }
}
