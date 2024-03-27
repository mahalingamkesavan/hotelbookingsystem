using MediatR;

namespace HotelBookingSystem.DAL.Queries
{
    public record GetById<T>(int id) : IRequest<T>;
}
