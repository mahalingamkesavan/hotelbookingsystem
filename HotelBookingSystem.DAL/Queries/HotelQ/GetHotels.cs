using HotelBookingSystem.Models.Entities;
using HotelBookingSystem.Models.FilteringModels;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.DAL.Queries.HotelQ
{
    public record GetHotels(
        Expression<Func<Hotel, bool>> Expression,
        BaseFilter? Filter) : IRequest<List<Hotel>>;
}
