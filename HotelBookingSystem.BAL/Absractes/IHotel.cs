using HotelBookingSystem.Models.FilteringModels;
using HotelBookingSystem.Models.ResponseModels;
using System.Linq.Expressions;

namespace HotelBookingSystem.BAL.Absractes
{
    public interface IHotel : IGeneric<HotelEntity>
    {
        Task<BaseResponse<IEnumerable<HotelEntity>>> Get(HotelFilter filter);
        Task<BaseResponse<IEnumerable<HotelEntity>>> GetForAdmin(HotelFilter? filter = null);
        Task<BaseResponse<object>> AnyHotel(HotelFilter filter,bool forSearch = false);
    }
}
