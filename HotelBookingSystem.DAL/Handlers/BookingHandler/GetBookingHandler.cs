using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.DAL.Queries.BookingQ;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.BookingHandler
{
    internal class GetBookingHandler : IRequestHandler<GetBooking, IEnumerable<Booking>>
    {
        public GetBookingHandler(HotelBookingContext context)
        {
            Context = context;
        }

        public readonly HotelBookingContext Context;
        public async Task<IEnumerable<Booking>> Handle(GetBooking request, CancellationToken cancellationToken)
        {
            //await Context.Bookings.Join(Context.OccupantDetails,
            //    b => b.Id,
            //    o => o.BookingId,
            //    (b, o) => b).ToListAsync();
            var result = await Context.Bookings.Where(request.Expression).ToListAsync();
            if (result.Count == 1)
            {
                var temp = result.ElementAtOrDefault(0);
                if (temp == null)
                {
                    return result;
                }
                Booking? booking = await Context
                    .Bookings
                    .IgnoreAutoIncludes()
                    .Include("OccupantDetails")
                    .Include("CreatedByNavigation")
                    .SingleOrDefaultAsync(x => x.Id == temp.Id);
                if (booking != null)
                {
                    return new List<Booking>() { booking };
                }
            }
            return result;
        }
    }
}
