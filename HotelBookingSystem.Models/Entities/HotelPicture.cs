using System;
using System.Collections.Generic;

namespace HotelBookingSystem.Models.Entities
{
    public partial class HotelPicture
    {
        public string Id { get; set; } = null!;
        public int HotelId { get; set; }
        public string ImageEndpoint { get; set; } = null!;

        public virtual Hotel Hotel { get; set; } = null!;
    }
}
