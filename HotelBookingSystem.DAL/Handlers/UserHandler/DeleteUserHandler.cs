using HotelBookingSystem.DAL.Commands.UserC;
using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.UserHandler
{
    internal class DeleteUserHandler : IRequestHandler<DeleteUser, bool>
    {
        private HotelBookingContext _Context;

        public DeleteUserHandler(HotelBookingContext context)
        {
            _Context = context;
        }
        public async Task<bool> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var user = await _Context.Users.Where(request.Expression).SingleOrDefaultAsync(cancellationToken: cancellationToken);
            if (user == null)
            {
                return false;
            }
            user.Status = TableConstants.InActive;
            _Context.SaveChanges();
            return true;
        }
    }
}
