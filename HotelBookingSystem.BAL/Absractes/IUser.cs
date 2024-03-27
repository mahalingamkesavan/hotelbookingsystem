using HotelBookingSystem.Models.ResponseModels;
using System.Linq.Expressions;

namespace HotelBookingSystem.BAL.Absractes
{
    public interface IUser : IGeneric<UserEntity>
    {
        Task<BaseResponse<UserEntity>> GetUser(string username);
        Task<BaseResponse<UserEntity>> CreateAdmin(UserEntity userEntity);
        Task<BaseResponse<UserEntity>> GetToken(LogIn logIn);
        Task<BaseResponse<UserEntity>> GetToken(UserEntity logIn);
    }
}
