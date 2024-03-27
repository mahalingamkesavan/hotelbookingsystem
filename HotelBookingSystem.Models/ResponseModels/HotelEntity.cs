using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.ResponseModels
{
    public class HotelEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(100)]
        public string Address { get; set; } = null!;
        [MaxLength(100)]
        public string City { get; set; } = null!;
        [MaxLength(100)]
        public string State { get; set; } = null!;
        [MaxLength(10)]
        public string Pincode { get; set; } = null!;

        public decimal HotelRating { get; set; }
        public string? Description { get; set; } = null;
        [ValidateNever]
        public IEnumerable<HotelRoomEntity>? Rooms { get; set; } = null;
        public IEnumerable<string>? Pictures { get; set; } = null;
        public Dictionary<string,string>? Aminities { get; set; } = null;
    }
}
