using System;
using System.Collections.Generic;

namespace HotelBookingSystem.Models.Entities
{
    public partial class User
    {
        public User()
        {
            BookingApprovedByNavigations = new HashSet<Booking>();
            BookingCreatedByNavigations = new HashSet<Booking>();
            BookingUpdatedByNavigations = new HashSet<Booking>();
            OccupantDetailCreatedByNavigations = new HashSet<OccupantDetail>();
            OccupantDetailUpdatedByNavigations = new HashSet<OccupantDetail>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Age { get; set; }
        public string Password { get; set; } = null!;
        public string Type { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ImageEndPoint { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Booking> BookingApprovedByNavigations { get; set; }
        public virtual ICollection<Booking> BookingCreatedByNavigations { get; set; }
        public virtual ICollection<Booking> BookingUpdatedByNavigations { get; set; }
        public virtual ICollection<OccupantDetail> OccupantDetailCreatedByNavigations { get; set; }
        public virtual ICollection<OccupantDetail> OccupantDetailUpdatedByNavigations { get; set; }
    }
}
