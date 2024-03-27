using System;
using System.Collections.Generic;

namespace HotelBookingSystem.Models.Entities
{
    public partial class OccupantDetail
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public int Age { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UdatedDate { get; set; }

        public virtual Booking Booking { get; set; } = null!;
        public virtual User CreatedByNavigation { get; set; } = null!;
        public virtual User UpdatedByNavigation { get; set; } = null!;
    }
}
