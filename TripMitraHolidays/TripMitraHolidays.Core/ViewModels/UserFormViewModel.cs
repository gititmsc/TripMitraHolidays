using System.ComponentModel.DataAnnotations;

namespace TripMitraHolidays.Core.ViewModels
{
    public class UserFormViewModel
    {
        // Encrypted user ID — null/empty means Create mode, set means Edit mode
        public string EncryptedId { get; set; }

        // Decoded integer ID — populated by controller, not posted from form
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required."), MaxLength(150)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required."), MaxLength(200)]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        public bool IsEditMode => !string.IsNullOrEmpty(EncryptedId);
    }
}
