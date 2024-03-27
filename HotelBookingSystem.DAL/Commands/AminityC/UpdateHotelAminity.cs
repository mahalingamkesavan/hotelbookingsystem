using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.DAL.Commands.AminityC
{
    public record UpdateHotelAminity(params Aminity[] Aminities) : IRequest<List<Aminity>>;

    public class UpdateHotelAminityHandler : IRequestHandler<UpdateHotelAminity, List<Aminity>>
    {
        private readonly HotelBookingContext _Context;

        public UpdateHotelAminityHandler(HotelBookingContext context)
        {
            this._Context = context;
        }
        public async Task<List<Aminity>> Handle(UpdateHotelAminity request, CancellationToken cancellationToken)
        {
            _Context.Aminities.UpdateRange(request.Aminities);
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
                    .ToListAsync(cancellationToken);
                return await result;
            }
            return Array.Empty<Aminity>().ToList();
        }
    }
}
