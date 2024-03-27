using HotelBookingSystem.Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.ResponseModels
{
    public class OccupantEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BookingId { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        public string LastName { get; set; } = null!;
        [StringLength(18)]
        public string Gender { get; set; } = null!;
        [AgeValidation]
        public int Age { get; set; }
        [Timestamp]
        public DateTime UdatedDate { get; set; }
    }
}
