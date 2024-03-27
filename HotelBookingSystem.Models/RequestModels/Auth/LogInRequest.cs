using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.RequestModels.Auth
{
    public class LogInRequest
    {
        [Required(ErrorMessage = "User Name is Required")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Password is Required")]
        [Display(Name = "Password")]
        [MaxLength(15)]
        public string Password { get; set; } = null!;
    }
}
