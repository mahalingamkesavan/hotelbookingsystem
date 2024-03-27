using HotelBookingSystem.DAL.Commands.UserC;
using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using HotelBookingSystem.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.UserHandler
{
    internal class EditUserHandler : IRequestHandler<EditUser, User>
    {
        private readonly HotelBookingContext _Context;

        public EditUserHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        public async Task<User> Handle(EditUser request, CancellationToken cancellationToken)
        {
            var res = _Context.Update(request.User);
            var numRowChange = await _Context.SaveChangesAsync(cancellationToken);
            if (numRowChange > 0)
            {
                return request.User;
            }
            throw new OperationFailedException(nameof(EditUser));
        }
    }
}
