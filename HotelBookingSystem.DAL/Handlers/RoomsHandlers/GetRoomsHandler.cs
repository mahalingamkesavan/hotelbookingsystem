using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.DAL.Queries.RoomsQ;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.RoomsHandlers
{
    internal class GetRoomsHandler : IRequestHandler<GetRooms, IEnumerable<HotelRoom>>
    {
        public GetRoomsHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        private readonly HotelBookingContext _Context;
        public async Task<IEnumerable<HotelRoom>> Handle(GetRooms request, CancellationToken cancellationToken)
        {
            var result = await _Context.HotelRooms.Where(request.Expression).ToListAsync();

            if (!result.Any())
                return null;
            return result;
        }
    }
}
