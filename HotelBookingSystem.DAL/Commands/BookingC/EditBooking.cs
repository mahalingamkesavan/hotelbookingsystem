using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.BookingC
{
    public record EditBooking(Booking Booking) : IRequest<Booking>;
}
