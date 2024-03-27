using HotelBookingSystem.Models.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.ResponseModels
{
    public class UserEntity
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
        public string? UserPic { get; set; }
        public string? Type { get; set; } = null;
    }
}
