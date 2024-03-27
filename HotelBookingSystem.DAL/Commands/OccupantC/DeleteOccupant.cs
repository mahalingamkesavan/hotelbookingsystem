using HotelBookingSystem.Models.Entities;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.DAL.Commands.OccupantC
{
    public record DeleteOccupant(Expression<Func<OccupantDetail, bool>> Expression) : IRequest<bool>;
}
