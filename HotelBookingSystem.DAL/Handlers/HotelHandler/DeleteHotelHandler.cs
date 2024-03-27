using HotelBookingSystem.DAL.Commands.HotelC;
using HotelBookingSystem.DAL.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.HotelHandler
{
    internal class DeleteHotelHandler : IRequestHandler<DeleteHotel, bool>
    {
        public DeleteHotelHandler(HotelBookingContext context)
        {
            Context = context;
        }

        public readonly HotelBookingContext Context;
        public async Task<bool> Handle(DeleteHotel request, CancellationToken cancellationToken)
        {
            var delHotel = await Context.Hotels.SingleOrDefaultAsync(request.Expression);
            if (delHotel == null)
            {
                return false;
            }
            Context.Hotels.Remove(delHotel);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
