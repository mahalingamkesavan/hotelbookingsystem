using HotelBookingSystem.Models.Entities;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.DAL.Commands.Rooms
{
    public record DeleteRooms(Expression<Func<HotelRoom, bool>> Expression) : IRequest<bool>;
}
