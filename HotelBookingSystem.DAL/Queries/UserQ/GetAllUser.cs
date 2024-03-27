using HotelBookingSystem.Models.Entities;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.DAL.Queries.UserQ
{
    public record GetAllUser(Expression<Func<User, bool>>? Expression = null) : IRequest<IEnumerable<User>>;
}
