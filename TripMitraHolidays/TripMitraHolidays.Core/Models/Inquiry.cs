using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripMitraHolidays.Core.Models
{
    [Table("Inquiries")]
    public class Inquiry
    {
        [Key]
        public int InquiryId { get; set; }

        [Required, MaxLength(150)]
        public string FullName { get; set; }

        [Required, MaxLength(20)]
        public string MobileNumber { get; set; }

        [Required, MaxLength(150)]
        public string EmailAddress { get; set; }

        public DateTime? TravelDate { get; set; }

        public int? NumberOfPersons { get; set; }

        [MaxLength(200)]
        public string PreferredDestination { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        //[Column(TypeName = "decimal(18,2)")]
        public decimal? Budget { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public Inquiry()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
