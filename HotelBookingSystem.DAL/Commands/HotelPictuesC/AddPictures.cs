using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.HotelPictuesC
{
    public record AddPictures(IEnumerable<HotelPicture> Pictures)
        : IRequest<bool>;
    public class AddPicturesHandler : IRequestHandler<AddPictures, bool>
    {
        public AddPicturesHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        private readonly HotelBookingContext _Context;
        public async Task<bool> Handle(AddPictures request, CancellationToken cancellationToken)
        {
            if (request.Pictures.Any(x=> string.IsNullOrEmpty(x.Id)))
            {
                foreach (var item in request.Pictures)
                {
                    if (string.IsNullOrEmpty(item.Id))
                        item.Id = Guid.NewGuid().ToString();
                }
            }
            await _Context.HotelPictures.AddRangeAsync(request.Pictures);
            await _Context.SaveChangesAsync();
            return true;
        }
    }
}
