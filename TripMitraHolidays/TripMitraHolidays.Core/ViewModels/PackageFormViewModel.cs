using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripMitraHolidays.Core.ViewModels
{
    public class PackageFormViewModel
    {
        public int PackageId { get; set; }

        // SECTION 1 — Basic Details
        [Required(ErrorMessage = "Package name is required")]
        [MaxLength(300)]
        [Display(Name = "Package Name")]
        public string PackageName { get; set; }

        [MaxLength(300)]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [MaxLength(200)]
        [Display(Name = "Destination")]
        public string Destination { get; set; }

        [MaxLength(200)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [MaxLength(200)]
        [Display(Name = "Starting City")]
        public string StartingCity { get; set; }

        [MaxLength(100)]
        [Display(Name = "Package Type")]
        public string PackageType { get; set; }

        [MaxLength(100)]
        [Display(Name = "Tour Category")]
        public string TourCategory { get; set; }

        // SECTION 2 — Pricing
        [Required(ErrorMessage = "Package price is required")]
        [Range(0.01, 99999999, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Actual Price (₹)")]
        public decimal PackagePrice { get; set; }

        [Display(Name = "Discount Price (₹)")]
        public decimal? DiscountPrice { get; set; }

        // SECTION 3 — Tour Details
        [Range(1, 365, ErrorMessage = "Days must be between 1 and 365")]
        [Display(Name = "Duration Days")]
        public int DurationDays { get; set; }

        [Range(0, 365, ErrorMessage = "Nights must be between 0 and 365")]
        [Display(Name = "Duration Nights")]
        public int DurationNights { get; set; }

        [MaxLength(10)]
        [Display(Name = "Hotel Rating")]
        public string HotelRating { get; set; }

        [MaxLength(100)]
        [Display(Name = "Meal Type")]
        public string MealType { get; set; }

        [MaxLength(100)]
        [Display(Name = "Transportation")]
        public string Transportation { get; set; }

        // SECTION 4 — Existing image paths (for Edit)
        public string ExistingThumbnailImage { get; set; }
        public string ExistingBannerImage { get; set; }
        public List<GalleryImageDto> ExistingGalleryImages { get; set; }

        // SECTION 5 — Description
        [MaxLength(1000)]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Full Description")]
        public string Description { get; set; }

        // SECTION 6-8 — Dynamic sections serialized as JSON
        public string ItinerariesJson { get; set; }
        public string InclusionsJson { get; set; }
        public string ExclusionsJson { get; set; }

        // SECTION 9 — Flags
        [Display(Name = "Featured Package")]
        public bool IsFeatured { get; set; }

        [Display(Name = "Popular Package")]
        public bool IsPopular { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Flight Included")]
        public bool IsFlightIncluded { get; set; }

        [Display(Name = "Visa Included")]
        public bool IsVisaIncluded { get; set; }

        // SECTION 10 — SEO
        [MaxLength(300)]
        [Display(Name = "Meta Title")]
        public string MetaTitle { get; set; }

        [MaxLength(500)]
        [Display(Name = "Meta Keywords")]
        public string MetaKeywords { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; }

        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        public PackageFormViewModel()
        {
            IsActive = true;
            DurationDays = 1;
            DurationNights = 0;
            ExistingGalleryImages = new List<GalleryImageDto>();
            ItinerariesJson = "[]";
            InclusionsJson = "[]";
            ExclusionsJson = "[]";
        }
    }

    public class GalleryImageDto
    {
        public int ImageId { get; set; }
        public string ImagePath { get; set; }
    }
}
