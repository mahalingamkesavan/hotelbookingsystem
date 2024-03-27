using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelBookingSystem.DAL.Queries.HotelPicturesQ
{
    public record GetPictures(Expression<Func<HotelPicture, bool>> Filter)
        : IRequest<IEnumerable<HotelPicture>>;
    internal class GetHotelPicturesHandler
        : IRequestHandler<GetPictures, IEnumerable<HotelPicture>>
    {
        public GetHotelPicturesHandler(HotelBookingContext context)
        {
            Context = context;
        }

        public HotelBookingContext Context { get; }

        public async Task<IEnumerable<HotelPicture>> Handle(GetPictures request, CancellationToken cancellationToken)
        {
            return await Context.HotelPictures.Where(request.Filter).ToListAsync();
        }
    }
}
