using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DAL.Queries.HotelPicturesQ
{
    public record GetPictuesById(string Id) : IRequest<IEnumerable<HotelPicture>>;
    internal class GetPictuesByIdHandler
        : IRequestHandler<GetPictuesById, IEnumerable<HotelPicture>>
    {
        public GetPictuesByIdHandler(HotelBookingContext context)
        {
            Context = context;
        }

        public HotelBookingContext Context { get; }

        public async Task<IEnumerable<HotelPicture>> Handle(GetPictuesById request, CancellationToken cancellationToken)
        {
            return await Context.HotelPictures.Where(x=>x.Id == request.Id).ToListAsync();
        }
    }
}
