using HotelBookingSystem.Models.Entities;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.DAL.Queries.BookingQ
{
    public record class GetBooking(Expression<Func<Booking, bool>> Expression) : IRequest<IEnumerable<Booking>>;
}
