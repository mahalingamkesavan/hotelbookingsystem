using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.UserC
{
    public record CreateUser(User User) : IRequest<User>;
}
