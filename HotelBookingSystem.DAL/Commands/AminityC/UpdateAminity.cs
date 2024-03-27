using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Commands.AminityC
{
    public record UpdateAminity(params Aminity[] Aminities) : IRequest<List<Aminity>>;

    public class UpdateAminityHandler : IRequestHandler<UpdateAminity, List<Aminity>>
    {
        private readonly HotelBookingContext _Context;

        public UpdateAminityHandler(HotelBookingContext context)
        {
            _Context = context;
        }
        public async Task<List<Aminity>> Handle(UpdateAminity request, CancellationToken cancellationToken)
        {

            _Context.Aminities.RemoveRange(request.Aminities);
            int rowsUpdated = await _Context.SaveChangesAsync();
            if (rowsUpdated > 0)
            {
                var result = _Context
                    .Aminities
                    .Where(x =>
                        request
                            .Aminities
                            .Any(y =>
                                y.Id == x.Id)
                            )
                    .ToListAsync();
                return await result;
            }
            return Array.Empty<Aminity>().ToList();
        }
    }
}
