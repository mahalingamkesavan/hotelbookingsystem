using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.HotelPictuesC
{
    public record RemovePictures(IEnumerable<HotelPicture> Pictures)
        : IRequest<bool>;
}
