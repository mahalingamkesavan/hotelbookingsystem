using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.DAL.Queries;
using MediatR;

namespace HotelBookingSystem.DAL.Handlers
{
    public class GetHandler<T> : IRequestHandler<GetById<T>, T>
        where T : class
    {
        public GetHandler(HotelBookingContext context)
        {
            Context = context;
        }

        private HotelBookingContext Context { get; }

        public async Task<T> Handle(GetById<T> request, CancellationToken cancellationToken)
        {
            T? result = await Context.Set<T>().FindAsync(request.id);
            if (result == null)
                return null;
            return result;
        }
    }
}
