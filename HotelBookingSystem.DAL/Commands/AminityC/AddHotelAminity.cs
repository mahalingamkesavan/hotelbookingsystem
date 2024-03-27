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
    public record AddHotelAminity(params HotelAminity[] Aminities) : IRequest<List<HotelAminity>>;

    public class AddHotelAminityHandler : IRequestHandler<AddHotelAminity, List<HotelAminity>>
    {
        private readonly HotelBookingContext _Context;

        public AddHotelAminityHandler(HotelBookingContext context)
        {
            this._Context = context;
        }
        public async Task<List<HotelAminity>> Handle(AddHotelAminity request, CancellationToken cancellationToken)
        {
            _Context.HotelAminities.AddRange(request.Aminities);
            int rowsUpdated = await _Context.SaveChangesAsync();
            if (rowsUpdated > 0)
            {
                var result = _Context
                    .HotelAminities
                    .Where(x =>
                        request
                            .Aminities
                            .Any(y =>
                                y.Aminityid == x.Aminityid)
                            )
                    .ToListAsync();
                return await result;
            }
            return Array.Empty<HotelAminity>().ToList();
        }
    }
}
