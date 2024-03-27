using HotelBookingSystem.Models.Entities;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.DAL.Queries.RoomsQ
{
    public record GetRooms(Expression<Func<HotelRoom, bool>> Expression) : IRequest<IEnumerable<HotelRoom>>;
}
