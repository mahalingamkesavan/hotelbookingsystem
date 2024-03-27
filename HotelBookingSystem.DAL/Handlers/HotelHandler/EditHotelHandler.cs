using HotelBookingSystem.DAL.Commands.HotelC;
using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Handlers.HotelHandler
{
    public class EditHotelHandler : IRequestHandler<EditHotel, Hotel>
    {
        public EditHotelHandler(HotelBookingContext context)
        {
            Context = context;
        }

        public readonly HotelBookingContext Context;

        public async Task<Hotel> Handle(EditHotel request, CancellationToken cancellationToken)
        {
            Hotel hotel = request.Hotel;
            if (hotel == null)
            {
                return null;
            }
            Context.Hotels.Update(hotel);
            await Context.SaveChangesAsync();
            return Context.Hotels.SingleOrDefault(x => x.Id == hotel.Id);
        }
    }
}
