using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.UserC
{
    public record EditUser(User User) : IRequest<User>;
}
