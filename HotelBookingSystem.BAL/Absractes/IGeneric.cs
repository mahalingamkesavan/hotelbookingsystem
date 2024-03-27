using HotelBookingSystem.Models.ResponseModels;
using System.Linq.Expressions;

namespace HotelBookingSystem.BAL.Absractes
{
    public interface IGeneric<TEntity> where TEntity : class
    {
        Task<BaseResponse<IEnumerable<TEntity>>> Get(Expression<Func<TEntity, bool>>? expression = null);
        Task<BaseResponse<TEntity>> GetById(int id);
        Task<BaseResponse<TEntity>> Create(TEntity entity, string? UserName = null);
        Task<BaseResponse<TEntity>> Update(TEntity entity, int id = 0, string? UserName = null);
        Task<BaseResponse<TEntity>> Delete(Expression<Func<TEntity, bool>> filter);
        Task<BaseResponse<TEntity>> DeleteById(int Id);
    }
}
