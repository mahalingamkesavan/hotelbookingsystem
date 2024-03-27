using System;
using System.Collections.Generic;

namespace HotelBookingSystem.Models.Entities
{
    public partial class Hotel
    {
        public Hotel()
        {
            Bookings = new HashSet<Booking>();
            HotelAminities = new HashSet<HotelAminity>();
            HotelDescriptions = new HashSet<HotelDescription>();
            HotelPictures = new HashSet<HotelPicture>();
            HotelRooms = new HashSet<HotelRoom>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Pincode { get; set; } = null!;
        public decimal HotelRating { get; set; }
        public string Status { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<HotelAminity> HotelAminities { get; set; }
        public virtual ICollection<HotelDescription> HotelDescriptions { get; set; }
        public virtual ICollection<HotelPicture> HotelPictures { get; set; }
        public virtual ICollection<HotelRoom> HotelRooms { get; set; }
    }
}
