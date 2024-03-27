using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.HotelC
{
    public record CreateHotel(Hotel Hotel) : IRequest<Hotel>;
    internal class CreateHotelHandler : IRequestHandler<CreateHotel, Hotel>
    {
        public CreateHotelHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        public HotelBookingContext _Context { get; }
        public async Task<Hotel> Handle(CreateHotel request, CancellationToken cancellationToken)
        {
            Hotel hotel = request.Hotel;
            _Context.Hotels.Add(request.Hotel);
            int rowCreated = await _Context.SaveChangesAsync();
            if (rowCreated > 0)
            {
                return _Context.Hotels.Where(x => x.Name == hotel.Name && x.Address == hotel.Address).FirstOrDefault();
            }
            return null;
        }
    }
}
