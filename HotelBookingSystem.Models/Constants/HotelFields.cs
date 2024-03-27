using HotelBookingSystem.Models.Entities;
using System.Linq.Expressions;

namespace HotelBookingSystem.Models.Constants
{
    public sealed class HotelFields
    {
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Address = "Address";
        public const string City = "City";
        public const string Pincode = "Pincode";
        public const string HotelRating = "HotelRating";
        public const string State = "State";

        public static Dictionary<string, Expression<Func<Hotel, object>>> HotelOrder { get; } =
            new()
            {
                {Id, x=>x.Id },
                {Name, x=>x.Name},
                {Address, x=>x.Address},
                {City, x=>x.City},
                {Pincode, x=>x.Pincode},
                {HotelRating, x=>x.HotelRating}
            };
    }
}
