using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.BookingC
{
    public record NewBooking(Booking Booking) : IRequest<Booking>;
}
