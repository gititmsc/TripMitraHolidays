using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripMitraHolidays.Core.Models
{
    [Table("Packages")]
    public class Package
    {
        [Key]
        public int PackageId { get; set; }

        [Required, MaxLength(300)]
        public string PackageName { get; set; }

        [Required, MaxLength(300)]
        public string Slug { get; set; }

        [MaxLength(1000)]
        public string ShortDescription { get; set; }

        public string Description { get; set; }

        //[Column(TypeName = "decimal(18,2)")]
        public decimal PackagePrice { get; set; }

        //[Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountPrice { get; set; }

        public int DurationDays { get; set; }
        public int DurationNights { get; set; }

        [MaxLength(200)]
        public string Destination { get; set; }

        [MaxLength(200)]
        public string Country { get; set; }

        [MaxLength(200)]
        public string StartingCity { get; set; }

        [MaxLength(100)]
        public string PackageType { get; set; }

        [MaxLength(100)]
        public string TourCategory { get; set; }

        [MaxLength(500)]
        public string ThumbnailImage { get; set; }

        [MaxLength(500)]
        public string BannerImage { get; set; }

        [MaxLength(10)]
        public string HotelRating { get; set; }

        [MaxLength(100)]
        public string MealType { get; set; }

        [MaxLength(100)]
        public string Transportation { get; set; }

        public bool IsFlightIncluded { get; set; }
        public bool IsVisaIncluded { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsPopular { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }

        [MaxLength(300)]
        public string MetaTitle { get; set; }

        [MaxLength(500)]
        public string MetaKeywords { get; set; }

        [MaxLength(1000)]
        public string MetaDescription { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<PackageImage> Images { get; set; }
        public virtual ICollection<PackageItinerary> Itineraries { get; set; }
        public virtual ICollection<PackageInclusion> Inclusions { get; set; }
        public virtual ICollection<PackageExclusion> Exclusions { get; set; }

        public Package()
        {
            Images = new HashSet<PackageImage>();
            Itineraries = new HashSet<PackageItinerary>();
            Inclusions = new HashSet<PackageInclusion>();
            Exclusions = new HashSet<PackageExclusion>();
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
