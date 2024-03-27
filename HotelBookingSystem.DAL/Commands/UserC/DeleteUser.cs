using HotelBookingSystem.Models.Entities;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.DAL.Commands.UserC
{
    public record DeleteUser(Expression<Func<User, bool>> Expression) : IRequest<bool>;
}
