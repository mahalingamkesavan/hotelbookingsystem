using HotelBookingSystem.Models.ResponseModels;
using System.Linq.Expressions;

namespace HotelBookingSystem.BAL.Absractes
{
    public interface IBooking : IGeneric<BookingEntity>
    {
        Task<BaseResponse<IEnumerable<BookingEntity>>> GetByUser
            (
                string userName,
                Expression<Func<BookingEntity, bool>>? expression = null
            );
    }
}
