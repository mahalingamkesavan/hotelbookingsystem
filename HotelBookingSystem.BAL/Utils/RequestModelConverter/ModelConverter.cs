using HotelBookingSystem.Models.Constants;
using HotelBookingSystem.Models.Entities;
using HotelBookingSystem.Models.ResponseModels;

namespace HotelBookingSystem.BAL.Utils.RequestModelConverter
{
    internal static partial class ModelConverter
    {
        public static HotelEntity ConvertToHotelEntity(this Hotel hotel)
        {
            HotelEntity entity = new HotelEntity
            {
                City = hotel.City,
                State = hotel.State,
                Pincode = hotel.Pincode,
                HotelRating = hotel.HotelRating,
                Name = hotel.Name,
                Id = hotel.Id,
                Address = hotel.Address,
                Description = hotel.Description
            };
            if (hotel.HotelRooms != null && hotel.HotelRooms.Count > 0)
            {
                List<HotelRoomEntity> HRentity = new List<HotelRoomEntity>();
                foreach (var item in hotel.HotelRooms)
                {
                    HRentity.Add(item.ConvertToHotelRoom());
                }
                entity.Rooms = HRentity;
            }
            List<string> pictureUrl = new();
            if (hotel.HotelPictures != null && hotel.HotelPictures.Count > 0)
            {
                foreach (var item in hotel.HotelPictures)
                {
                    pictureUrl.Add(FileEndPoint.BaseUrl+item.ImageEndpoint);
                }
            }
            else
            {
                pictureUrl.Add(FileEndPoint.BaseUrl+FileEndPoint.HotelPictureNotFound);
            }
            entity.Pictures = pictureUrl;

            Dictionary<string, string> Aminities = new();

            if (hotel.HotelAminities != null && hotel.HotelAminities.Count > 0)
            {
                foreach (var item in hotel.HotelAminities)
                {
                    if(!Aminities.Any(x=>x.Key == item.Aminity.AminityName))
                        Aminities.Add(item.Aminity.AminityName,item.Aminity.AminityDescription ?? "");
                }
                entity.Aminities = Aminities;
            }

            return entity;
        }

        public static HotelRoomEntity ConvertToHotelRoom(this HotelRoom room)
        {
            HotelRoomEntity entity = new HotelRoomEntity
            {
                BedSize = room.BedSize,
                Description = room.Description,
                HotelId = room.HotelId,
                Id = room.Id,
                Rate = room.Rate,
                RoomName = room.RoomName,
                RoomType = room.RoomType
            };
            return entity;
        }

        public static UserEntity ConvertToUserEntity(this User user)
        {
            UserEntity entity = new UserEntity
            {
                Username = user.Username,
                Age = user.Age,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Type = user.Type,
                UserPic = user.ImageEndPoint!=null ? FileEndPoint.BaseUrl + user.ImageEndPoint : null,
            };
            return entity;
        }
        public static BookingEntity ConvertToBookingEntity(this Booking booking)
        {
            BookingEntity entity = new BookingEntity
            {
                Id = booking.Id,
                EndDate = booking.EndDate,
                HotelId = booking.HotelId,
                RoomId = booking.RoomId,
                RoomName = booking.Room?.RoomName ?? null,
                HotelName = booking.Hotel?.Name ?? null,
                Status = booking.Status,
                StartDate = booking.StartDate,
                BookingNo = booking.BookingNo,
                UdatedDate = booking.UdatedDate,
                CreatedBy = booking.CreatedByNavigation?.FirstName ?? null,
            };
            if (booking.OccupantDetails != null)
            {
                List<OccupantEntity> OccEntity = new List<OccupantEntity>();
                foreach (var item in booking.OccupantDetails)
                {
                    OccEntity.Add(item.ConvertToOccupantEntity());
                }
                entity.occupants = OccEntity;
            }
            return entity;
        }
        public static OccupantEntity ConvertToOccupantEntity(this OccupantDetail occupant)
        {
            OccupantEntity entity = new OccupantEntity
            {
                Age = occupant.Age,
                BookingId = occupant.BookingId,
                FirstName = occupant.FirstName,
                Gender = occupant.Gender,
                Id = occupant.Id,
                LastName = occupant.LastName,
                UdatedDate = occupant.UdatedDate
            };
            return entity;
        }
    }
}
