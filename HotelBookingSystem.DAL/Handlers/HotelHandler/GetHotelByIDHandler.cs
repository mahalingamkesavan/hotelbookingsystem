using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.DAL.Queries.HotelQ;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.HotelHandler
{
    public class GetHotelByIDHandler : IRequestHandler<GetHotelByID, Hotel>
    {
        public GetHotelByIDHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        public HotelBookingContext _Context { get; }

        public async Task<Hotel> Handle(GetHotelByID request, CancellationToken cancellationToken)
        {
            var Hotel2 = await _Context.Hotels
                .IgnoreAutoIncludes()
                .Select(x => new
                {
                    x.Name, x.Address, x.City,x.Id,
                    x.State,x.HotelRating,
                    endPoint = x.HotelPictures.Where(y=>y.HotelId == x.Id).Select(x=>x.ImageEndpoint).ToList(),
                }).ToListAsync();
            var Hotel = await _Context.Hotels
                .IgnoreAutoIncludes()
                .Include(h => h.HotelRooms)
                .ThenInclude(r=>r.RoomPictures)
                .Include(h => h.HotelAminities)
                .ThenInclude(a => a.Aminity)
                .Where(H => H.Id == request.Id)
                .FirstOrDefaultAsync();

            return Hotel ?? throw new Exception("Object not found");
        }
    }
}
