using System;
using System.ComponentModel.DataAnnotations;

namespace TripMitraHolidays.Core.ViewModels
{
    public class InquiryFormViewModel
    {
        [Required(ErrorMessage = "Full name is required.")]
        [MaxLength(150, ErrorMessage = "Full name cannot exceed 150 characters.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [MaxLength(20)]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Please enter a valid 10-digit Indian mobile number.")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [MaxLength(150)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Travel Date")]
        public DateTime? TravelDate { get; set; }

        [Range(1, 100, ErrorMessage = "Number of persons must be between 1 and 100.")]
        [Display(Name = "Number of Persons")]
        public int? NumberOfPersons { get; set; }

        [MaxLength(200)]
        [Display(Name = "Preferred Destination")]
        public string PreferredDestination { get; set; }

        [MaxLength(100)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid budget amount.")]
        [Display(Name = "Budget (₹)")]
        public decimal? Budget { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }

        // Used to pre-fill when coming from a package detail page
        public string PackageName { get; set; }
    }
}
