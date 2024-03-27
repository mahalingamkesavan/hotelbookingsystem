using System;
using System.Collections.Generic;

namespace HotelBookingSystem.Models.Entities
{
    public partial class Booking
    {
        public Booking()
        {
            OccupantDetails = new HashSet<OccupantDetail>();
        }

        public int Id { get; set; }
        public int? HotelId { get; set; }
        public int? RoomId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UdatedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? BookingNo { get; set; }
        public string? Status { get; set; }

        public virtual User? ApprovedByNavigation { get; set; }
        public virtual User? CreatedByNavigation { get; set; }
        public virtual Hotel? Hotel { get; set; }
        public virtual HotelRoom? Room { get; set; }
        public virtual User? UpdatedByNavigation { get; set; }
        public virtual ICollection<OccupantDetail> OccupantDetails { get; set; }
    }
}
