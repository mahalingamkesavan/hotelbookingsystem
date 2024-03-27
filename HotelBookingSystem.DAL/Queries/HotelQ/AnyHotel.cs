using HotelBookingSystem.Models.Entities;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.DAL.Queries.HotelQ
{
    public record AnyHotel(Expression<Func<Hotel, bool>> filter,bool ForSearch = false) : IRequest<object>;
}
