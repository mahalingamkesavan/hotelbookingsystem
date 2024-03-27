using HotelBookingSystem.DAL.Commands.Rooms;
using HotelBookingSystem.DAL.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.RoomsHandlers
{
    internal class DeleteRoomsHandler : IRequestHandler<DeleteRooms, bool>
    {
        public DeleteRoomsHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        private readonly HotelBookingContext _Context;
        public async Task<bool> Handle(DeleteRooms request, CancellationToken cancellationToken)
        {
            var Rooms = await _Context.HotelRooms.Where(request.Expression).ToListAsync();

            if (Rooms.Count == 0)
            {
                return false;
            }

            _Context.RemoveRange(Rooms);

            await _Context.SaveChangesAsync();

            return true;

        }
    }
}
