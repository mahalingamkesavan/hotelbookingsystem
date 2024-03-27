using HotelBookingSystem.Models.Entities;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.DAL.Queries.UserQ
{
    public record GetUser(Expression<Func<User, bool>> Filter) : IRequest<User>;
}
