using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.HotelPictuesC
{
    public record UpdatePicture(HotelPicture Picture) : IRequest<bool>;
}
