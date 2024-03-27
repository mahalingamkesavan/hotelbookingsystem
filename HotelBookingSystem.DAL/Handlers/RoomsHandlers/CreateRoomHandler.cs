using HotelBookingSystem.DAL.Commands.Rooms;
using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.RoomsHandlers
{
    internal class CreateRoomHandler : IRequestHandler<CreateRoom, HotelRoom>
    {
        public CreateRoomHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        private readonly HotelBookingContext _Context;
        public async Task<HotelRoom> Handle(CreateRoom request, CancellationToken cancellationToken)
        {
            HotelRoom room = request.Room;
            if (room == null)
                return null;
            int? id = await _Context.HotelRooms.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync() + 1;
            room.Id = id ?? 1;
            await _Context.HotelRooms.AddAsync(room);
            await _Context.SaveChangesAsync();
            return room;
        }
    }
}
