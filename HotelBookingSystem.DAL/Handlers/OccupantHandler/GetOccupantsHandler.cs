using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.DAL.Queries.OccupantQ;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.OccupantHandler
{
    internal class GetOccupantsHandler : IRequestHandler<GetOccupants, IEnumerable<OccupantDetail>>
    {
        public GetOccupantsHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        private readonly HotelBookingContext _Context;
        public async Task<IEnumerable<OccupantDetail>> Handle(GetOccupants request, CancellationToken cancellationToken)
        {
            if (request.Expression == null)
            {
                return await _Context.OccupantDetails.ToListAsync();
            }
            var result = await _Context.OccupantDetails.Where(request.Expression).ToListAsync();

            return result;
        }
    }
}
