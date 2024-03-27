using HotelBookingSystem.Models.Entities;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.DAL.Commands.BookingC
{
    public record DeleteBooking(Expression<Func<Booking, bool>> Expression) : IRequest<bool>;
}
