using HotelBookingSystem.DAL.Commands.OccupantC;
using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.OccupantHandler
{
    internal class AddOccupantHandler : IRequestHandler<AddOccupant, OccupantDetail>
    {
        public AddOccupantHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        private readonly HotelBookingContext _Context;
        public async Task<OccupantDetail> Handle(AddOccupant request, CancellationToken cancellationToken)
        {
            OccupantDetail occupant = request.Occupant;
            if (occupant == null)
            {
                return null;
            }

            int? id = _Context.OccupantDetails.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault() + 1;

            occupant.Id = id ?? 1;

            var user = await _Context.Users.SingleOrDefaultAsync(x => x.Username == occupant.CreatedByNavigation.Username);

            if (user == null)
            {
                return null;
            }

            int createdById = user.Id;

            occupant.CreatedDate = DateTime.Now;

            await _Context.OccupantDetails.AddAsync(occupant);
            await _Context.SaveChangesAsync();
            return occupant;
        }
    }
}
