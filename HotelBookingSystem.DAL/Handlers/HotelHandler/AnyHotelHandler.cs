using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.DAL.Queries.HotelQ;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.HotelHandler
{
    public class AnyHotelHandler : IRequestHandler<AnyHotel, object>
    {
        public AnyHotelHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        public HotelBookingContext _Context { get; }
        public async Task<object> Handle(AnyHotel request, CancellationToken cancellationToken)
        {
            if(request.ForSearch)
                return await _Context.Hotels.Where(request.filter).Select(x => new
                {
                    x.Name,
                    x.City
                }).ToListAsync(cancellationToken: cancellationToken);
            else
                return await _Context.Hotels.Where(request.filter).CountAsync();
        }
    }
}
