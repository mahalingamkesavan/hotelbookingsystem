using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.RequestModels.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
