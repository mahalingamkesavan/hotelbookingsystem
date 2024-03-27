using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.DAL.Queries.AminityQ
{
    public record GetHotelAminity(Expression<Func<HotelAminity, bool>>? filter = null) : IRequest<IEnumerable<HotelAminity>>;
    public class GetHotelAminityHandler : IRequestHandler<GetHotelAminity, IEnumerable<HotelAminity>>
    {
        public GetHotelAminityHandler(HotelBookingContext context)
        {
            Context = context;
        }

        public HotelBookingContext Context { get; }

        public async Task<IEnumerable<HotelAminity>> Handle(GetHotelAminity request, CancellationToken cancellationToken)
        {
            var res = Context.HotelAminities;
            if (request.filter != null)
            {
                res.Where(request.filter);
            }
            res.Include(x => x.Aminity.AminityName);
            return await res.ToListAsync(cancellationToken);
        }
    }
}
