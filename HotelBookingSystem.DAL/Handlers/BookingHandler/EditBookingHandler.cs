using HotelBookingSystem.DAL.Commands.BookingC;
using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.BookingHandler
{
    internal class EditBookingHandler : IRequestHandler<EditBooking, Booking>
    {
        private readonly HotelBookingContext _Context;

        public EditBookingHandler(HotelBookingContext context)
        {
            _Context = context;
        }
        public async Task<Booking> Handle(EditBooking request, CancellationToken cancellationToken)
        {
            var booking = request.Booking;
            booking.UdatedDate = DateTime.Now;
            booking.UpdatedBy = (await _Context.Users.FirstOrDefaultAsync(x => x.Username == booking.UpdatedByNavigation.Username) ?? throw new InvalidDataException()).Id;
            _Context.Bookings.Update(booking);
            await _Context.SaveChangesAsync();
            return await _Context.Bookings.SingleOrDefaultAsync(x => x.Id == request.Booking.Id);
        }
    }
}
