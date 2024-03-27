using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.ResponseModels
{
    public class HotelRoomEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int HotelId { get; set; }
        [MaxLength(30)]
        public string? RoomType { get; set; }
        [MaxLength(100)]
        public string? RoomName { get; set; }
        public decimal? Rate { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }
        [MaxLength(30)]
        public string? BedSize { get; set; }
    }
}
