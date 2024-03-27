using System;
using System.Collections.Generic;

namespace HotelBookingSystem.Models.Entities
{
    public partial class HotelDescription
    {
        public int Id { get; set; }
        public int Hotelid { get; set; }
        public string? Description { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
    }
}
