using HotelBookingSystem.BAL.Absractes;
using HotelBookingSystem.DAL.Commands.AminityC;
using HotelBookingSystem.DAL.Queries.AminityQ;
using HotelBookingSystem.Models.Entities;
using HotelBookingSystem.Models.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HotelBookingSystem.BAL.Concrets
{
    public class AminityServices : IAminity
    {
        private readonly ISender _Mediator;

        public AminityServices(ISender mediator)
        {
            this._Mediator = mediator;
        }

        public async Task<BaseResponse<IEnumerable<HotelAminity>>> AddAminityToHotel(params HotelAminity[] entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var res = await _Mediator.Send(new AddHotelAminity(entity));
            return BaseResponse<IEnumerable<HotelAminity>>.GetSuccessResult(res);
        }

        public async Task<BaseResponse<Aminity>> Create(Aminity entity, string? UserName = null)
        {
            if (entity == null) 
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var res = await _Mediator.Send(new AddAminity(entity));
            return BaseResponse<Aminity>.GetSuccessResult(res.FirstOrDefault());
        }

        public Task<BaseResponse<Aminity>> Delete(Expression<Func<Aminity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IEnumerable<HotelAminity>>> DeleteAminityFromHotel(int id)
        {
            bool res = await _Mediator.Send(new DeleteAminity(id));
            var newRes = GetHotelAminity();
            if (res)
                return await newRes;
            return BaseResponse<IEnumerable<HotelAminity>>.GetError("No Data Found");
        }

        public Task<BaseResponse<Aminity>> DeleteById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IEnumerable<Aminity>>> Get(Expression<Func<Aminity, bool>>? expression = null)
        {
            IEnumerable<Aminity>? res = await _Mediator.Send(new GetAminity(expression));
            return BaseResponse<IEnumerable<Aminity>>.GetSuccessResult(res);
        }

        public async Task<BaseResponse<Aminity>> GetById(int id)
        {
            IEnumerable<Aminity>? res = await _Mediator.Send(new GetAminity(x=>x.Id == id));
            if(res.Any())
                return BaseResponse<Aminity>.GetSuccessResult(res.First());
            return BaseResponse<Aminity>.GetSuccessResult("No Result Found");
        }

        public async Task<BaseResponse<IEnumerable<HotelAminity>>> GetHotelAminity(Expression<Func<HotelAminity, bool>>? filter = null)
        {
            IEnumerable<HotelAminity>? res = await _Mediator.Send(new GetHotelAminity(filter));
            return BaseResponse<IEnumerable<HotelAminity>>.GetSuccessResult(res);
        }

        public async Task<BaseResponse<HotelAminity>> GetHotelAminity(int id)
        {
            IEnumerable<HotelAminity>? res = await _Mediator.Send(new GetHotelAminity(x=>x.Id == id));
            if (res.Any())
                return BaseResponse<HotelAminity>.GetSuccessResult(res.First());
            return BaseResponse<HotelAminity>.GetSuccessResult("No Result Found");
        }

        public async Task<BaseResponse<Aminity>> Update(Aminity entity, int id = 0, string? UserName = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var res = await _Mediator.Send(new UpdateAminity(entity));
            return BaseResponse<Aminity>.GetSuccessResult(res.FirstOrDefault());
        }
    }
}
