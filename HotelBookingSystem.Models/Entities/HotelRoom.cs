using System;
using System.Collections.Generic;

namespace HotelBookingSystem.Models.Entities
{
    public partial class HotelRoom
    {
        public HotelRoom()
        {
            Bookings = new HashSet<Booking>();
            RoomPictures = new HashSet<RoomPicture>();
        }

        public int Id { get; set; }
        public int HotelId { get; set; }
        public string? RoomType { get; set; }
        public string? RoomName { get; set; }
        public decimal? Rate { get; set; }
        public string? Description { get; set; }
        public string? BedSize { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<RoomPicture> RoomPictures { get; set; }
    }
}
