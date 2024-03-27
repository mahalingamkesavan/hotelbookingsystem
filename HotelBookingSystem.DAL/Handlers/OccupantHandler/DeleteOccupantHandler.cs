using HotelBookingSystem.DAL.Commands.OccupantC;
using HotelBookingSystem.DAL.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.OccupantHandler
{
    internal class DeleteOccupantHandler : IRequestHandler<DeleteOccupant, bool>
    {
        public DeleteOccupantHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        private readonly HotelBookingContext _Context;
        public async Task<bool> Handle(DeleteOccupant request, CancellationToken cancellationToken)
        {
            var occpant = await _Context.OccupantDetails.Where(request.Expression).ToListAsync();

            if (occpant.Count == 0)
            {
                return false;
            }

            _Context.OccupantDetails.RemoveRange(occpant);

            await _Context.SaveChangesAsync();

            return true;
        }
    }
}
