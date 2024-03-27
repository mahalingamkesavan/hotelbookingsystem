using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.DAL.Queries.UserQ;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.UserHandler
{
    internal class GetAllUserHandler : IRequestHandler<GetAllUser, IEnumerable<User>>
    {
        public GetAllUserHandler(HotelBookingContext context)
        {
            Context = context;
        }

        public readonly HotelBookingContext Context;

        public async Task<IEnumerable<User>> Handle(GetAllUser request, CancellationToken cancellationToken)
        {
            if (request.Expression == null)
                return await Context.Users.Select(x => new User()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Age = x.Age,
                    ImageEndPoint = x.ImageEndPoint,
                    CreatedDate = x.CreatedDate,
                    Type = x.Type,
                    Username = x.Username,
                    Status = x.Status,
                }).ToListAsync();
            return await Context.Users.Where(request.Expression).Select(x => new User()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Age = x.Age,
                ImageEndPoint = x.ImageEndPoint,
                CreatedDate = x.CreatedDate,
                Type = x.Type,
                Username = x.Username,
                Status = x.Status,
            }).ToListAsync();
        }
    }
}
