using System;
using System.Collections.Generic;

namespace HotelBookingSystem.Models.Entities
{
    public partial class RoomPicture
    {
        public string Id { get; set; } = null!;
        public int RoomId { get; set; }
        public string ImageEndpoint { get; set; } = null!;

        public virtual HotelRoom Room { get; set; } = null!;
    }
}
