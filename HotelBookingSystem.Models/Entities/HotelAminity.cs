using System;
using System.Collections.Generic;

namespace HotelBookingSystem.Models.Entities
{
    public partial class HotelAminity
    {
        public int Id { get; set; }
        public int Hotelid { get; set; }
        public int Aminityid { get; set; }

        public virtual Aminity Aminity { get; set; } = null!;
        public virtual Hotel Hotel { get; set; } = null!;
    }
}
