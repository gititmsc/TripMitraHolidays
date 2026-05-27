using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripMitraHolidays.Core.Models
{
    [Table("PackageItineraries")]
    public class PackageItinerary
    {
        [Key]
        public int ItineraryId { get; set; }

        public int PackageId { get; set; }

        public int DayNumber { get; set; }

        [Required, MaxLength(300)]
        public string Title { get; set; }

        public string Description { get; set; }

        [ForeignKey("PackageId")]
        public virtual Package Package { get; set; }
    }
}
