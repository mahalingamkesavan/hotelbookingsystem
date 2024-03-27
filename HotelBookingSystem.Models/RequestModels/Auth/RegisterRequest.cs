using HotelBookingSystem.Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.RequestModels.Auth
{
    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [AgeValidation(18)]
        public int Age { get; set; }
        [Required]
        [Display(Name = "Password")]
        [MaxLength(15)]
        public string Password { get; set; } = null!;
        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;
        public string Type { get; set; } = null!;
    }
}
