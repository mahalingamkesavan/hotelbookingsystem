using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Commands.AminityC
{
    public record AddAminity(params Aminity[] Aminities) : IRequest<List<Aminity>>;

    public class AddAminityHandler : IRequestHandler<AddAminity, List<Aminity>>
    {
        private readonly HotelBookingContext _Context;

        public AddAminityHandler(HotelBookingContext context)
        {
            this._Context = context;
        }
        public async Task<List<Aminity>> Handle(AddAminity request, CancellationToken cancellationToken)
        {
            _Context.Aminities.AddRange(request.Aminities);
            int rowsUpdated = await _Context.SaveChangesAsync();
            if (rowsUpdated > 0)
            {
                var result = _Context
                    .Aminities
                    .Where(x =>
                        request
                            .Aminities
                            .Any(y =>
                                y.AminityName == x.AminityName)
                            )
                    .ToListAsync();
                return await result;
            }
            return Array.Empty<Aminity>().ToList();
        }
    }
}
