using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Commands.HotelAminityC
{
    public record UpdateHotelAminity(params HotelAminity[] Aminities) : IRequest<List<HotelAminity>>;

    public class UpdateHotelAminityHandler : IRequestHandler<UpdateHotelAminity, List<HotelAminity>>
    {
        private readonly HotelBookingContext _Context;

        public UpdateHotelAminityHandler(HotelBookingContext context)
        {
            this._Context = context;
        }
        public async Task<List<HotelAminity>> Handle(UpdateHotelAminity request, CancellationToken cancellationToken)
        {

            _Context.HotelAminities.RemoveRange(request.Aminities);
            int rowsUpdated = await _Context.SaveChangesAsync();
            if (rowsUpdated > 0)
            {
                var result = _Context
                    .HotelAminities
                    .Where(x =>
                        request
                            .Aminities
                            .Any(y =>
                                y.Id == x.Id)
                            )
                    .ToListAsync();
                return await result;
            }
            return null;
        }
    }
}
