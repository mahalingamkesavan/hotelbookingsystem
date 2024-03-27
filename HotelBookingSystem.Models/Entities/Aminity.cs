using System;
using System.Collections.Generic;

namespace HotelBookingSystem.Models.Entities
{
    public partial class Aminity
    {
        public Aminity()
        {}

        public int Id { get; set; }
        public string AminityName { get; set; } = null!;
        public string? AminityDescription { get; set; }

        public virtual ICollection<HotelAminity>? HotelAminities { get; set; } = null;
    }
}
