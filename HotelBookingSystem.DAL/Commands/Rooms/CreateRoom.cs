using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.Rooms
{
    public record CreateRoom(HotelRoom Room) : IRequest<HotelRoom>;
}
