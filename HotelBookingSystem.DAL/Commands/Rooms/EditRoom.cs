using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.Rooms
{
    public record EditRoom(HotelRoom Room) : IRequest<HotelRoom>;
}
