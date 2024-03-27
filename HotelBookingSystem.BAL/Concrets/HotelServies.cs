using HotelBookingSystem.BAL.Absractes;
using HotelBookingSystem.BAL.Utils;
using HotelBookingSystem.BAL.Utils.RequestModelConverter;
using HotelBookingSystem.BAL.Utils.ResponseModelConverter;
using HotelBookingSystem.DAL.Commands.HotelC;
using HotelBookingSystem.DAL.Queries.HotelQ;
using HotelBookingSystem.Models.Constants;
using HotelBookingSystem.Models.Entities;
using HotelBookingSystem.Models.FilteringModels;
using HotelBookingSystem.Models.ResponseModels;
using HotelBookingSystem.Utils.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HotelBookingSystem.BAL.Concrets
{
    public class HotelServies : IHotel
    {
        private readonly ISender _Mediatr;

        public HotelServies(ISender mediatr)
        {
            this._Mediatr = mediatr;
        }

        public async Task<BaseResponse<HotelEntity>> Create(HotelEntity entity, string? UserName = null)
        {
            Hotel hotel = entity.ConvertToHotel();

            var res = await _Mediatr.Send(new CreateHotel(hotel));
            if (res == null)
                return BaseResponse<HotelEntity>.GetError("");

            return BaseResponse<HotelEntity>.GetSuccessResult(res.ConvertToHotelEntity());
        }

        public async Task<BaseResponse<HotelEntity>> Delete(Expression<Func<HotelEntity, bool>> filter)
        {
            var newfilter = filter.ReplaceParameter<HotelEntity, Hotel>();

            var flag = await _Mediatr.Send(new DeleteHotel(newfilter));

            if(flag)
            return BaseResponse<HotelEntity>.GetSuccessResult(""); 
            else
                return BaseResponse<HotelEntity>.GetError("");
        }

        public async Task<BaseResponse<HotelEntity>> DeleteById(int Id)
        {
            var flag = await _Mediatr.Send(new DeleteHotel(x => x.Id == Id));

            if (flag)
                return BaseResponse<HotelEntity>.GetSuccessResult("");
            else
                return BaseResponse<HotelEntity>.GetError("");
        }

        public async Task<BaseResponse<IEnumerable<HotelEntity>>> Get(HotelFilter filter)
        {

            var res = await _Mediatr.Send(new GetHotels(GetPredicate(filter),filter));

            var totalCount = await AnyHotelObject(filter);

            res.AsQueryable().AsNoTracking();

            if (!res.Any())
                return BaseResponse<IEnumerable<HotelEntity>>.GetError("");

            List<HotelEntity> AllHotel = new List<HotelEntity>();

            foreach (var item in res)
            {
                AllHotel.Add(item.ConvertToHotelEntity());
            }
            return BaseResponse<IEnumerable<HotelEntity>>.GetSuccessResult(AllHotel,(int)totalCount);
        }

        public async Task<BaseResponse<IEnumerable<HotelEntity>>> GetForAdmin(HotelFilter? filter = null)
        {

            var res = await _Mediatr.Send(new GetHotels(GetPredicate(filter,"Admin"), filter));

            if(!res.Any())
                return BaseResponse<IEnumerable<HotelEntity>>.GetError("");

            List<HotelEntity> AllHotel = new List<HotelEntity>();

            foreach (var item in res)
            {
                AllHotel.Add(item.ConvertToHotelEntity());
            }

            var totalCount = await AnyHotelObject(filter);

            return BaseResponse<IEnumerable<HotelEntity>>.GetSuccessResult(AllHotel, (int)totalCount);
        }

        public async Task<BaseResponse<HotelEntity>> GetById(int id)
        {
            var res = await _Mediatr.Send(new GetHotelByID(id));

            if(res == null)
                return BaseResponse<HotelEntity>.GetError();

            return BaseResponse<HotelEntity>.GetSuccessResult(res.ConvertToHotelEntity());
        }

        public async Task<BaseResponse<object>> AnyHotel(HotelFilter? filter,bool forSearch =false )
        {
            var anyHotel = await _Mediatr.Send(
                new AnyHotel(GetPredicate(filter),forSearch));
            if(forSearch)
                return BaseResponse<object>.GetSuccessResult(anyHotel);
            var res = BaseResponse<object>.GetSuccessResult(null,(int)anyHotel);
            res.NumResult = (int)res.NumResult;
            return res;
        }

        public async Task<BaseResponse<HotelEntity>> Update(HotelEntity entity, int id, string? UserName = null)
        {
            Hotel hotel = entity.ConvertToHotel();

            var res = await _Mediatr.Send(new EditHotel(hotel));
            if (res == null)
                return BaseResponse<HotelEntity>.GetError();

            return BaseResponse<HotelEntity>.GetSuccessResult(res.ConvertToHotelEntity());
        }

        public Task<BaseResponse<IEnumerable<HotelEntity>>> Get(Expression<Func<HotelEntity, bool>>? expression = null)
        {
            return Task.FromResult(BaseResponse<IEnumerable<HotelEntity>>.GetError("Method Not Found"));
        }
        private Expression<Func<Hotel, bool>> GetPredicate(HotelFilter? filter,string forUser = "Customer")
        {

            Expression<Func<Hotel, bool>> predicate = x => true;

            if (filter != null)
            {
                if (filter.Search != null)
                {
                    predicate = predicate.AndExpression(x =>
                    (
                        x.Name.StartsWith(filter.Search) ||
                        x.City.StartsWith(filter.Search) ||
                        x.Pincode == filter.Search
                    ));
                }
                if (filter.HotelName != null)
                {
                    predicate = predicate.AndExpression(x =>
                    (
                        x.Name.StartsWith(filter.HotelName)
                    ));
                }
                if (filter.HotelLocation != null)
                {
                    predicate = predicate.AndExpression(x => (x.City.StartsWith(filter.HotelLocation)));
                }
                if (filter.HotelPincode != null)
                {
                    predicate = predicate.AndExpression(x => (x.Pincode == filter.HotelPincode));
                }
                if (filter.Ratings.Count != 6)
                    predicate = predicate.AndExpression(x =>
                    filter.Ratings.Contains(((int)x.HotelRating)));
                if (forUser == "Admin")
                {
                    if (filter.MinHotelRate != 0)
                        predicate = predicate.AndExpression(x =>
                        x.HotelRooms.Min(x => x.Rate) >= filter.MinHotelRate
                        );
                    if (filter.MaxHotelRate != int.MaxValue)
                        predicate = predicate.AndExpression(x =>
                        x.HotelRooms.Min(x => x.Rate) <= filter.MaxHotelRate
                        );
                }
                else
                {
                    predicate = predicate.AndExpression(x =>
                        x.HotelRooms.Min(x => x.Rate) >= filter.MinHotelRate &&
                        x.HotelRooms.Min(x => x.Rate) <= filter.MaxHotelRate
                        );
                }
            }
            else
                if (forUser == Roles.Admin)
                    predicate = x => true;
            return predicate;
        }
        private async Task<object> AnyHotelObject(HotelFilter? filter, bool forSearch = false, string forUser = "")
        {
            var anyHotel = await _Mediatr.Send(
                new AnyHotel(GetPredicate(filter, forUser), forSearch));
            return anyHotel;
        }
    }
    
}
