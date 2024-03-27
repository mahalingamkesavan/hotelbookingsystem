using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.HotelAminityC
{
    public record AddHotelAminity(params HotelAminity[] Aminity) : IRequest<List<HotelAminity>>;

    public class AddHotelAminityHandler : IRequestHandler<AddHotelAminity, List<HotelAminity>>
    {
        private readonly HotelBookingContext _Context;

        public AddHotelAminityHandler(HotelBookingContext context)
        {
            this._Context = context;
        }
        public async Task<List<HotelAminity>> Handle(AddHotelAminity request, CancellationToken cancellationToken)
        {
            _Context.HotelAminities.AddRange(request.Aminity);
            int rowsUpdated = await _Context.SaveChangesAsync();
            if (rowsUpdated > 0)
            {
                return request.Aminity.ToList();
            }
            return null;
        }
    }
}
