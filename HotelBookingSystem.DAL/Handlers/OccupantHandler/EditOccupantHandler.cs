using HotelBookingSystem.DAL.Commands.OccupantC;
using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.OccupantHandler
{
    internal class EditOccupantHandler : IRequestHandler<EditOccupant, OccupantDetail>
    {
        public EditOccupantHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        private readonly HotelBookingContext _Context;
        public async Task<OccupantDetail> Handle(EditOccupant request, CancellationToken cancellationToken)
        {
            OccupantDetail occupant = request.Occupant;
            if (occupant == null)
            {
                return null;
            }
            occupant.CreatedDate = DateTime.Now;

            var user = await _Context.Users.SingleOrDefaultAsync(x => x.Username == occupant.UpdatedByNavigation.Username);

            if (user == null)
                return null;

            int updateid = user.Id;

            _Context.OccupantDetails.Update(occupant);

            await _Context.SaveChangesAsync();

            return occupant;
        }
    }
}
