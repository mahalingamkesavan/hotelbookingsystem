using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.HotelC
{
    public record EditHotel(Hotel Hotel) : IRequest<Hotel>;
}
