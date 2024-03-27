using HotelBookingSystem.Models.Entities;
using MediatR;
using System.Linq.Expressions;

namespace HotelBookingSystem.DAL.Commands.HotelC
{
    public record DeleteHotel(Expression<Func<Hotel, bool>> Expression) : IRequest<bool>;
}
