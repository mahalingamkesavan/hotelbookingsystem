using HotelBookingSystem.Authendication;
using HotelBookingSystem.BAL.Absractes;
using HotelBookingSystem.BAL.Utils;
using HotelBookingSystem.BAL.Utils.RequestModelConverter;
using HotelBookingSystem.BAL.Utils.ResponseModelConverter;
using HotelBookingSystem.DAL.Commands.UserC;
using HotelBookingSystem.DAL.Queries.UserQ;
using HotelBookingSystem.Models.Constants;
using HotelBookingSystem.Models.Entities;
using HotelBookingSystem.Models.ResponseModels;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.BAL.Concrets
{
    public class UserServies : IUser
    {
        private readonly ISender _Mediatr;
        private readonly IAuthendicate Authendicate;

        public UserServies(ISender mediatr, IAuthendicate authendicate)
        {
            this._Mediatr = mediatr;
            this.Authendicate = authendicate;
        }

        public async Task<BaseResponse<UserEntity>> Create(UserEntity entity, string? UserName)
        {
            User user = entity.ConvertToUser();
            var AddedUser = await _Mediatr.Send(new CreateUser(user));
            string token = Authendicate.GenerateToken(user);
            var res = BaseResponse<UserEntity>.GetSuccessResult(AddedUser.ConvertToUserEntity());
            res.Token = token;
            return res;
        }

        public async Task<BaseResponse<UserEntity>> Delete(Expression<Func<UserEntity, bool>> filter)
        {
            var NewFilter = filter.ReplaceParameter<UserEntity, User>();
            var flag = await _Mediatr.Send(new DeleteUser(NewFilter));
            if (flag)
                return BaseResponse<UserEntity>.GetSuccessResult("");
            return BaseResponse<UserEntity>.GetError();
        }

        public async Task<BaseResponse<IEnumerable<UserEntity>>> Get(Expression<Func<UserEntity, bool>>? filter = null)
        {
            var NewFilter = filter.ReplaceParameter<UserEntity, User>();
            var users = await _Mediatr.Send(new GetAllUser(NewFilter));
            if (users != null)
            {
                List<UserEntity> Usersentity = new List<UserEntity>();
                foreach (var item in users)
                {
                    Usersentity.Add(item.ConvertToUserEntity());
                }
                return BaseResponse<IEnumerable<UserEntity>>.GetSuccessResult(Usersentity); ;
            }
            throw new Exception("No user found");
        }

        public async Task<BaseResponse<UserEntity>> GetById(int id)
        {
            var user = await _Mediatr.Send(new GetUser(X => X.Id == id));
            if (user != null)
            {
                return BaseResponse<UserEntity>.GetSuccessResult(user.ConvertToUserEntity());
            }
            throw new Exception("No user found");
        }

        public async Task<BaseResponse<UserEntity>> Update(UserEntity entity, int id = 0, string? UserName = "")
        {
            User user = entity.ConvertToUser();

            var EditedUser = await _Mediatr.Send(new EditUser(user));

            return BaseResponse<UserEntity>.GetSuccessResult(EditedUser.ConvertToUserEntity());
        }

        public async Task<BaseResponse<UserEntity>> GetUser(string userName)
        {
            var user = await _Mediatr.Send
                (
                new GetUser
                    (
                    x=>x.Username == userName && 
                    x.Status == "Active"
                    )
                );
            if (user != null)
            {
                var res = BaseResponse<UserEntity>.GetSuccessResult(user.ConvertToUserEntity());
                return res;
            }
            throw new Exception("No user found");
        }

        public async Task<BaseResponse<UserEntity>> GetToken(LogIn logIn)
        {
            var user = await _Mediatr.Send(new GetUser(x => x.Username == logIn.Username && x.Password == logIn.Password));
            if (user != null)
            {
                var res = BaseResponse<UserEntity>.GetSuccessResult(Authendicate.GenerateToken(user));
                res.Result = user.ConvertToUserEntity();
                return res;
            }
            throw new Exception("No user found");
        }


        public async Task<BaseResponse<UserEntity>> GetToken(UserEntity logIn)
        {
            var user = await _Mediatr.Send(new GetUser(x => x.Username == logIn.Username && x.Password == logIn.Password));
            if (user != null)
            {
                var res = BaseResponse<UserEntity>.GetSuccessResult(Authendicate.GenerateToken(user));
                res.Result = user.ConvertToUserEntity();
                return res;
            }
            throw new Exception("No user found");
        }

        public async Task<BaseResponse<UserEntity>> DeleteById(int Id)
        {
            var flag = await _Mediatr.Send(new DeleteUser(x => x.Id == Id));
            if (flag)
                return BaseResponse<UserEntity>.GetSuccessResult("");
            return BaseResponse<UserEntity>.GetError();
        }

        public async Task<BaseResponse<UserEntity>> CreateAdmin(UserEntity userEntity)
        {
            User user = userEntity.ConvertToUser();
            user.Type = Roles.Admin;
            var AddedUser = await _Mediatr.Send(new CreateUser(user));
            string token = Authendicate.GenerateToken(user);
            var res = BaseResponse<UserEntity>.GetSuccessResult(AddedUser.ConvertToUserEntity());
            res.Token = token;
            return res;
        }
    }
}
