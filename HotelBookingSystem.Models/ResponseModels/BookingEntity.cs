using HotelBookingSystem.Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.ResponseModels
{
    public class BookingEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? HotelId { get; set; }
        [Required]
        public string? HotelName { get; set; }
        [Required]
        public int? RoomId { get; set; }
        [Required]
        public string? RoomName { get; set; }
        [Required]
        [Timestamp]
        public DateTime? StartDate { get; set; }
        [Required]
        [Timestamp]
        public DateTime? EndDate { get; set; }
        public string? CreatedBy { get; set; }
        //public string? UdatedBy { get; set; }
        [Timestamp]
        public DateTime? UdatedDate { get; set; }
        public string? BookingNo { get; set; }
        public string? Status { get; set; }
        [EmurableValidation]
        public IEnumerable<OccupantEntity>? occupants { get; set; }
    }
}
