using HotelBookingSystem.DAL.Commands.BookingC;
using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Handlers.BookingHandler
{
    internal class NewBookingHandler : IRequestHandler<NewBooking, Booking>
    {
        private readonly HotelBookingContext _Context;
        public NewBookingHandler(HotelBookingContext context)
        {
            _Context = context;
        }
        public async Task<Booking> Handle(NewBooking request, CancellationToken cancellationToken)
        {
            Booking booking = request.Booking;
            if (booking == null)
            {
                return null;
            }
            DateTime date = DateTime.Now;

            int? id = 1;
            id = await _Context.Bookings
                .OrderByDescending(x => x.Id)
                .Select(x => x.Id)
                .FirstOrDefaultAsync(); ;
            booking.Id = id ?? 1;
            booking.CreatedDate = date;
            booking.BookingNo = GenerateBookingNo(date, booking.CreatedByNavigation?.Username ?? "");
            await _Context.Bookings.AddAsync(booking);
            await _Context.SaveChangesAsync();
            var result = await _Context.Bookings.SingleOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return null;
            }
            return result;
        }
        private string GenerateBookingNo(DateTime date, string UserName)
        {
            string result = string.Empty;
            result += date.DayOfYear.ToString();
            result += date.Day.ToString();
            result += date.Minute.ToString();
            result += date.Millisecond.ToString();
            result += date.Second.ToString();
            result += date.Hour.ToString();
            result += date.Minute.ToString();
            return result;
        }
    }
}
