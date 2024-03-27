using HotelBookingSystem.BAL.Absractes;
using HotelBookingSystem.BAL.Utils;
using HotelBookingSystem.BAL.Utils.RequestModelConverter;
using HotelBookingSystem.BAL.Utils.ResponseModelConverter;
using HotelBookingSystem.DAL.Commands.BookingC;
using HotelBookingSystem.DAL.Queries.BookingQ;
using HotelBookingSystem.DAL.Queries.UserQ;
using HotelBookingSystem.Models.Entities;
using HotelBookingSystem.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq.Expressions;
#nullable disable
namespace HotelBookingSystem.BAL.Concrets
{
    public class BookingServices : IBooking
    {
        private readonly ISender _Mediator;

        public BookingServices(ISender mediator)
        {
            this._Mediator = mediator;
        }

        public async Task<BaseResponse<BookingEntity>> Create(BookingEntity entity, string Username)
        {
            Booking booking = entity.ConvertToBooKing();
            booking.CreatedBy = await GetUserId(Username);
            var result = await _Mediator.Send(new NewBooking(booking));
            return BaseResponse<BookingEntity>.GetSuccessResult(result.ConvertToBookingEntity());
        }

        public async Task<BaseResponse<BookingEntity>> Delete(Expression<Func<BookingEntity, bool>> filter)
        {
            var Newfilter = filter.ReplaceParameter<BookingEntity, Booking>();
            var res = await _Mediator.Send(new DeleteBooking(Newfilter));
            if (res)
                return BaseResponse<BookingEntity>.GetSuccessResult("Removed successfully");
            else
                return BaseResponse<BookingEntity>.GetError("");
        }

        public async Task<BaseResponse<BookingEntity>> DeleteById(int Id)
        {
            var res = await _Mediator.Send(new DeleteBooking(x => x.Id == Id));
            if(res)
                return BaseResponse<BookingEntity>.GetSuccessResult("Removed successfully");
            else
                return BaseResponse<BookingEntity>.GetError("");

        }

        public async Task<BaseResponse<IEnumerable<BookingEntity>>> Get(Expression<Func<BookingEntity, bool>> expression)
        {
            var Newfilter = expression.ReplaceParameter<BookingEntity, Booking>();
            var Bookings = await _Mediator.Send(new GetBooking(Newfilter));
            if(!Bookings.Any())
                return BaseResponse<IEnumerable<BookingEntity>>.GetError("No Data found",StatusCodes.Status204NoContent);
            List<BookingEntity> entitys = new List<BookingEntity>();
            foreach (var Booking in Bookings)
            {
                entitys.Add(Booking.ConvertToBookingEntity());
            }
            return BaseResponse<IEnumerable<BookingEntity>>.GetSuccessResult(entitys);
        }

        public async Task<BaseResponse<IEnumerable<BookingEntity>>> GetByUser
            (
                string userName,
                Expression<Func<BookingEntity, bool>> expression = null
            )
        {
            var Newfilter = expression.ReplaceParameter<BookingEntity, Booking>();
            if (Newfilter == null)
            {
                Newfilter = x => x.CreatedByNavigation.Username == userName;
            }
            var Bookings = await _Mediator.Send(new GetBooking(Newfilter));
            if (!Bookings.Any())
                return BaseResponse<IEnumerable<BookingEntity>>.GetError("No content found",StatusCodes.Status204NoContent);
            List <BookingEntity> entitys = new List<BookingEntity>();
            foreach (var Booking in Bookings)
            {
                entitys.Add(Booking.ConvertToBookingEntity());
            }
            return BaseResponse<IEnumerable<BookingEntity>>.GetSuccessResult(entitys);
        }

        public async Task<BaseResponse<BookingEntity>> GetById(int id)
        {
            var Bookings = await _Mediator.Send(new GetBooking(x => x.Id == id));
            if (!Bookings.Any())
            {
                return BaseResponse<BookingEntity>.GetError("");
            }
            var res = Bookings.FirstOrDefault().ConvertToBookingEntity();
            return BaseResponse<BookingEntity>.GetSuccessResult(res);
        }

        public async Task<BaseResponse<BookingEntity>> Update(BookingEntity entity, int id, string UserName)
        {
            Booking booking = entity.ConvertToBooKing();
            User user = new User();
            user.Username = UserName;
            booking.UpdatedByNavigation = user;
            var result = await _Mediator.Send(new EditBooking(booking));
            if (result == null)
                return BaseResponse<BookingEntity>.GetError("");
            return BaseResponse<BookingEntity>.GetSuccessResult(result.ConvertToBookingEntity());
        }
        private async Task<int> GetUserId(string username)
        {
            var userId = await _Mediator.Send(new GetAllUser(x => x.Username == username));
            if (userId == null)
                throw new Exception("No User Found");
            return userId.FirstOrDefault().Id;
        }
    }
}
