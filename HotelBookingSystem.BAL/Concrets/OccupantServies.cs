using HotelBookingSystem.BAL.Absractes;
using HotelBookingSystem.BAL.Utils;
using HotelBookingSystem.BAL.Utils.RequestModelConverter;
using HotelBookingSystem.BAL.Utils.ResponseModelConverter;
using HotelBookingSystem.DAL.Commands.OccupantC;
using HotelBookingSystem.DAL.Queries.OccupantQ;
using HotelBookingSystem.DAL.Queries.UserQ;
using HotelBookingSystem.Models.Entities;
using HotelBookingSystem.Models.ResponseModels;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.BAL.Concrets
{
    internal class OccupantServies : IOccupant
    {
        private readonly ISender _Mediatr;

        public OccupantServies(ISender mediatr)
        {
            this._Mediatr = mediatr;
        }

        public async Task<BaseResponse<OccupantEntity>> Create(OccupantEntity entity, string? UserName = null)
        {
            if(UserName ==null)
                throw new ArgumentNullException(nameof(UserName));
            OccupantDetail occupant = entity.ConvertToOccupant();
            occupant.CreatedBy = await GetUserId(UserName);
            var result = await _Mediatr.Send(new AddOccupant(occupant));
            if (result == null)
                return BaseResponse<OccupantEntity>.GetError("Failed");
            return BaseResponse < OccupantEntity > .GetSuccessResult(result.ConvertToOccupantEntity());
        }

        public async Task<BaseResponse<OccupantEntity>> Delete(Expression<Func<OccupantEntity, bool>> filter)
        {
            var newfilter = filter.ReplaceParameter<OccupantEntity, OccupantDetail>();
            var result = await _Mediatr.Send(new DeleteOccupant(newfilter));
            if (!result)
                return BaseResponse<OccupantEntity>.GetError("Failed");
            return BaseResponse < OccupantEntity > .GetSuccessResult("Success");
        }

        public async Task<BaseResponse<OccupantEntity>> DeleteById(int Id)
        {
            var result = await _Mediatr.Send(new DeleteOccupant(x => x.Id == Id));
            if (!result)
                return BaseResponse<OccupantEntity>.GetError("Failed");
            return BaseResponse<OccupantEntity>.GetSuccessResult("Success");
        }

        public async Task<BaseResponse<IEnumerable<OccupantEntity>>> Get(Expression<Func<OccupantEntity, bool>>? expression)
        {
            var newfilter = expression?.ReplaceParameter<OccupantEntity, OccupantDetail>();
            newfilter ??= x => true; 
            var result = await _Mediatr.Send(new GetOccupants(newfilter));
            if (result == null || !result.Any())
            {
                return BaseResponse<IEnumerable<OccupantEntity>>.GetError("Failed");
            }
            List<OccupantEntity> entities = new List<OccupantEntity>();
            foreach (var item in result)
            {
                entities.Add(item.ConvertToOccupantEntity());
            }
            return BaseResponse<IEnumerable<OccupantEntity>>.GetSuccessResult(entities);
        }

        public async Task<BaseResponse<OccupantEntity>> GetById(int id)
        {
            var Occpant = await _Mediatr.Send(new GetOccupants(x => x.Id == id));
            if (Occpant.Any() && Occpant!=null)
                return BaseResponse<OccupantEntity>.GetSuccessResult(Occpant.First().ConvertToOccupantEntity());
            return BaseResponse<OccupantEntity>.GetError("Failed");
        }

        public async Task<BaseResponse<OccupantEntity>> Update(OccupantEntity entity, int id, string? UserName)
        {
            OccupantDetail occupant = entity.ConvertToOccupant();
            occupant.Id = id;
            occupant.CreatedBy = await GetUserId(UserName);
            var result = await _Mediatr.Send(new EditOccupant(occupant));
            if (result == null)
                return BaseResponse<OccupantEntity>.GetError("Failed");
            return BaseResponse<OccupantEntity>.GetSuccessResult(result.ConvertToOccupantEntity());
        }
        private async Task<int> GetUserId(string username)
        {
            var user = await _Mediatr.Send(new GetAllUser(x => x.Username == username));
            if (user.Any())
            {
                return user.First().Id;
            }
            throw new Exception("UnAuthorized");
        }
    }
}
