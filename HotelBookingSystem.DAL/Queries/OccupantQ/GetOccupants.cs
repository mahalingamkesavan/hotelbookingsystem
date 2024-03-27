using HotelBookingSystem.Models.Entities;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.DAL.Queries.OccupantQ
{
    public record class GetOccupants(Expression<Func<OccupantDetail, bool>> Expression) : IRequest<IEnumerable<OccupantDetail>>;
}
