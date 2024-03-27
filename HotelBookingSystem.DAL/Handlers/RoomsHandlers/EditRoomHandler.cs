using HotelBookingSystem.DAL.Commands.Rooms;
using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Handlers.RoomsHandlers
{
    internal class EditRoomHandler : IRequestHandler<EditRoom, HotelRoom>
    {
        public EditRoomHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        private readonly HotelBookingContext _Context;
        public async Task<HotelRoom> Handle(EditRoom request, CancellationToken cancellationToken)
        {
            HotelRoom room = request.Room;
            if (room == null)
            {
                return null;
            }

            //HotelRoom ? oldroom = await _Context.HotelRooms.SingleOrDefaultAsync(x=>x.Id == room.Id);

            //if (oldroom==null)
            //{
            //    return null;
            //}

            //oldroom.RoomName = room.RoomName ;

            _Context.HotelRooms.Update(room);

            await _Context.SaveChangesAsync();

            return room;
        }
    }
}
