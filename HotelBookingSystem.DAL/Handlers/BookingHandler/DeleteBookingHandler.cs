using HotelBookingSystem.DAL.Commands.BookingC;
using HotelBookingSystem.DAL.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.BookingHandler
{
    internal class DeleteBookingHandler : IRequestHandler<DeleteBooking, bool>
    {
        public DeleteBookingHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        private readonly HotelBookingContext _Context;
        public async Task<bool> Handle(DeleteBooking request, CancellationToken cancellationToken)
        {
            var result = await _Context.Bookings.Where(request.Expression).ToListAsync();
            if (!result.Any())
            {
                return false;
            }
            _Context.Bookings.RemoveRange(result);
            await _Context.SaveChangesAsync();
            return true;
        }
    }
}
