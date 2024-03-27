using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.ResponseModels
{
    public class CommonFields
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

    }
}
