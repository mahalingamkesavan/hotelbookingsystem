using HotelBookingSystem.Models.Entities;
using HotelBookingSystem.Models.ResponseModels;
using System.Linq.Expressions;

namespace HotelBookingSystem.BAL.Absractes
{
    internal interface IAminity : IGeneric<Aminity>
    {
        Task<BaseResponse<IEnumerable<HotelAminity>>> GetHotelAminity(Expression<Func<HotelAminity,bool>>? filter = null);
        Task<BaseResponse<HotelAminity>> GetHotelAminity(int id);
        Task<BaseResponse<IEnumerable<HotelAminity>>> AddAminityToHotel(params HotelAminity[] hotel);
        Task<BaseResponse<IEnumerable<HotelAminity>>> DeleteAminityFromHotel(int id);
    }
}
