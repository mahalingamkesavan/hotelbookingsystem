using HotelBookingSystem.Models.Entities;
using HotelBookingSystem.Models.ResponseModels;

namespace HotelBookingSystem.BAL.Utils.ResponseModelConverter
{
    internal static partial class ModelConverter
    {
        public static Hotel ConvertToHotel(this HotelEntity entity)
        {
            Hotel hotel = new Hotel
            {
                Address = entity.Address,
                City = entity.City,
                HotelRating = entity.HotelRating,
                Id = entity.Id,
                Name = entity.Name,
                Pincode = entity.Pincode,
                State = entity.State
            };
            if (entity.Rooms != null && entity.Rooms.Any())
            {
                List<HotelRoom> hotelRooms = new List<HotelRoom>();
                foreach (var item in entity.Rooms)
                {
                    hotelRooms.Add(item.ConvertToHotelRoom());
                }
                hotel.HotelRooms = hotelRooms;
            }
            return hotel;
        }
        public static HotelRoom ConvertToHotelRoom(this HotelRoomEntity entity)
        {
            HotelRoom room = new HotelRoom
            {
                BedSize = entity.BedSize,
                Description = entity.Description,
                HotelId = entity.HotelId,
                Id = entity.Id,
                Rate = entity.Rate,
                RoomName = entity.RoomName,
                RoomType = entity.RoomType
            };
            return room;
        }
        public static User ConvertToUser(this UserEntity entity)
        {
            User user = new User
            {
                Type = entity.Type,
                Password = entity.Password,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                Email = entity.Email,
                Age = entity.Age,
                Username = entity.Username,
            };
            return user;
        }
        public static Booking ConvertToBooKing(this BookingEntity entity)
        {
            Booking booking = new Booking
            {
                Id = entity.Id,
                BookingNo = entity.BookingNo,
                EndDate = entity.EndDate,
                HotelId = entity.HotelId,
                RoomId = entity.RoomId,
                StartDate = entity.StartDate,
                Status = entity.Status
            };
            if (entity.occupants != null)
            {
                List<OccupantDetail> Occupants = new List<OccupantDetail>();
                foreach (var item in entity.occupants)
                {
                    Occupants.Add(item.ConvertToOccupant());
                }
                booking.OccupantDetails = Occupants;
            }
            return booking;
        }
        public static OccupantDetail ConvertToOccupant(this OccupantEntity entity)
        {
            OccupantDetail occupant = new OccupantDetail
            {
                Age = entity.Age,
                BookingId = entity.BookingId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Gender = entity.Gender,
                Id = entity.Id
            };
            return occupant;
        }
    }
}
