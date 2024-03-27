using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.DAL.Queries.HotelQ;
using HotelBookingSystem.Models.Constants;
using HotelBookingSystem.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelBookingSystem.DAL.Handlers.HotelHandler
{
    public class GetHotelsHandler : IRequestHandler<GetHotels, List<Hotel>>
    {
        public GetHotelsHandler(HotelBookingContext context)
        {
            _Context = context;
        }

        public HotelBookingContext _Context { get; }

        public async Task<List<Hotel>> Handle(GetHotels request, CancellationToken cancellationToken)
        {
            Dictionary<string, Expression<Func<Hotel, object>>> _HotelOrder
            = new Dictionary<string, Expression<Func<Hotel, object>>>
            {
                {HotelFields.Id,x=>x.Id},
                {HotelFields.Name,x=>x.Name},
                {HotelFields.Address,x=>x.Address},
                {HotelFields.City,x=>x.City},
                {HotelFields.Pincode,x=>x.Pincode},
                {HotelFields.State,x=>x.State},
                { HotelFields.HotelRating, x=>x.HotelRating},
                {"RoomCount",x=>x.HotelRooms.Count},
                { "MinRoomRate", x => x.HotelRooms.Min(x => x.Rate) ?? 0 },
                { "MaxRoomRate", x => x.HotelRooms.Max(x => x.Rate) ?? 0 },
            };

            //var result = _Context.Hotels;
            var result = _Context.Hotels
                    .Where(request.Expression);

            List<string> orderBy = request.Filter.OrderBy;


            if (orderBy.Count != 0)
            {
                foreach (var item in orderBy)
                {
                    if (!_HotelOrder.Keys.Contains(item))
                        throw new Exception("Invalid Sort Name");
                }
                if (request.Filter.IsAscending)
                {
                    var orderedList = result.OrderBy(_HotelOrder[orderBy[0]]);
                    for (int i = 1; i < orderBy.Count; i++)
                    {
                        orderedList = orderedList.ThenBy(_HotelOrder[orderBy[i]]);
                    }
                    result = orderedList;
                }
                else
                {
                    var orderedList = result.OrderByDescending(_HotelOrder[orderBy[0]]);
                    for (int i = 1; i < orderBy.Count; i++)
                    {
                        orderedList = orderedList.ThenByDescending(_HotelOrder[orderBy[i]]);
                    }
                    result = orderedList;
                }
            }

            result = result.Include(x => x.HotelPictures.Take(1));

            var res = await result
                .Skip(request.Filter.Limit * (request.Filter.Page - 1))
                .Take(request.Filter.Limit)
                .AsNoTracking()
                .ToListAsync();


            return res;
        }
    }
}
