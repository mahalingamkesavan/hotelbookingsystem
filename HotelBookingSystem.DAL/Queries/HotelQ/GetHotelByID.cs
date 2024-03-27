using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Queries.HotelQ
{
    public record GetHotelByID(int Id) : IRequest<Hotel>;
}
