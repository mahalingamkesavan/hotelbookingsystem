using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.DAL.Queries.UserQ;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.UserHandler
{
    public class GetUserHandler : IRequestHandler<GetUser, User>
    {
        private readonly HotelBookingContext _Context;

        public GetUserHandler(HotelBookingContext context)
        {
            this._Context = context;
        }

        public async Task<User> Handle(GetUser request, CancellationToken cancellationToken)
        {
            var user = await _Context.Users.Where(request.Filter).Select(x => new User()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Age= x.Age,
                ImageEndPoint= x.ImageEndPoint,
                CreatedDate = x.CreatedDate,
                Type= x.Type,
                Username= x.Username,
                Status= x.Status,
            }).FirstOrDefaultAsync();
            if (user != null)
                return user;
            else
                throw new Exception("No user found");
        }
    }
}
