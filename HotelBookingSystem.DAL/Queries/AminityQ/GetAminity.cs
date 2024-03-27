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
    public record GetAminity(Expression<Func<Aminity,bool>>? filter=null) : IRequest<IEnumerable<Aminity>>;
    public class GetAminityHandler : IRequestHandler<GetAminity, IEnumerable<Aminity>>
    {
        public GetAminityHandler(HotelBookingContext context)
        {
            Context = context;
        }

        public HotelBookingContext Context { get; }

        public async Task<IEnumerable<Aminity>> Handle(GetAminity request, CancellationToken cancellationToken)
        {
            var res = Context.Aminities;
            if(request.filter != null)
            {
                res.Where(request.filter);
            }
            return await res.ToListAsync();
        }
    }
}
