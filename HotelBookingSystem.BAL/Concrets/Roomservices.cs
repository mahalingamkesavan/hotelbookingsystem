using HotelBookingSystem.BAL.Absractes;
using HotelBookingSystem.BAL.Utils;
using HotelBookingSystem.BAL.Utils.RequestModelConverter;
using HotelBookingSystem.BAL.Utils.ResponseModelConverter;
using HotelBookingSystem.DAL.Commands.Rooms;
using HotelBookingSystem.DAL.Queries.RoomsQ;
using HotelBookingSystem.Models.Entities;
using HotelBookingSystem.Models.ResponseModels;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.BAL.Concrets
{
    internal class Roomservices : IRoom
    {
        private readonly ISender _Mediatr;

        public Roomservices(ISender mediatr)
        {
            this._Mediatr = mediatr;
        }

        public async Task<BaseResponse<HotelRoomEntity>> Create(HotelRoomEntity entity, string? UserName = null)
        {
            HotelRoom room = entity.ConvertToHotelRoom();
            var result = await _Mediatr.Send(new CreateRoom(room));
            if(result==null)
                return BaseResponse<HotelRoomEntity>.GetError();
            return BaseResponse<HotelRoomEntity>.GetSuccessResult(result.ConvertToHotelRoom());
        }

        public async Task<BaseResponse<HotelRoomEntity>> Delete(Expression<Func<HotelRoomEntity, bool>> filter)
        {
            var NewFilter = filter.ReplaceParameter<HotelRoomEntity, HotelRoom>();
            var flag = await _Mediatr.Send(new DeleteRooms(NewFilter)); 
            if (flag)
                return BaseResponse<HotelRoomEntity>.GetSuccessResult("");
            else
                return BaseResponse<HotelRoomEntity>.GetError("");
        }

        public async Task<BaseResponse<HotelRoomEntity>> DeleteById(int Id)
        {
            var flag = await _Mediatr.Send(new DeleteRooms(x => x.Id == Id));
            if (flag)
                return BaseResponse<HotelRoomEntity>.GetSuccessResult("");
            else
                return BaseResponse<HotelRoomEntity>.GetError("");
        }

        public async Task<BaseResponse<IEnumerable<HotelRoomEntity>>> Get(Expression<Func<HotelRoomEntity, bool>>? expression)
        {
            if(expression == null)
                return BaseResponse<IEnumerable<HotelRoomEntity>>.GetError();
            var anewfilter = expression.ReplaceParameter<HotelRoomEntity, HotelRoom>();
            var rooms = await _Mediatr.Send(new GetRooms(anewfilter));
            if (rooms == null || !rooms.Any())
            {
                return BaseResponse<IEnumerable<HotelRoomEntity>>.GetError();
            }
            List<HotelRoomEntity> entitys = new List<HotelRoomEntity>();
            foreach (var item in rooms)
            {
                entitys.Add(item.ConvertToHotelRoom());
            }
            return BaseResponse<IEnumerable<HotelRoomEntity>>.GetSuccessResult(entitys);
        }

        public async Task<BaseResponse<HotelRoomEntity>> GetById(int id)
        {
            var result = await _Mediatr.Send(new GetRooms(x => x.Id == id));
            if (result != null && result.Count() == 1)
            {
                return BaseResponse<HotelRoomEntity>.GetSuccessResult(result.First().ConvertToHotelRoom());
            }
            return BaseResponse<HotelRoomEntity>.GetError();
        }

        public async Task<BaseResponse<HotelRoomEntity>> Update(HotelRoomEntity entity, int id = 0, string? UserName = null)
        {
            var room = entity.ConvertToHotelRoom();
            var result = await _Mediatr.Send(new EditRoom(room));
            if(result == null)
                return BaseResponse<HotelRoomEntity>.GetError();
            return BaseResponse<HotelRoomEntity>.GetSuccessResult(result.ConvertToHotelRoom());
        }
    }
}
