using HotelBookingSystem.DAL.Commands.UserC;
using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.UserHandler
{
    public class CreateUserHandler : IRequestHandler<CreateUser, User>
    {
        private readonly HotelBookingContext _Context;

        public CreateUserHandler(HotelBookingContext context)
        {
            this._Context = context;
        }

        public async Task<User> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            User user = request.User;
            int? id = await _Context.Users.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync() + 1;
            user.Id = id ?? 1;
            user.CreatedDate = DateTime.Now;
            await _Context.Users.AddAsync(user);
            await _Context.SaveChangesAsync();
            user.Password = string.Empty;
            return user;
        }
    }
}
