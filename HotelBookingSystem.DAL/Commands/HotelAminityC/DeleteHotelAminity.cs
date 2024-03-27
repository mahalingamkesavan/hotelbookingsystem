using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.HotelAminityC
{
    public record DeleteHotelAminity(params HotelAminity[] Aminity) : IRequest<bool>;

    public class DeleteHotelAminityHandler : IRequestHandler<DeleteHotelAminity, bool>
    {

        private readonly HotelBookingContext _Context;

        public DeleteHotelAminityHandler(HotelBookingContext context)
        {
            this._Context = context;
        }
        public async Task<bool> Handle(DeleteHotelAminity request, CancellationToken cancellationToken)
        {
            _Context.HotelAminities.RemoveRange(request.Aminity);
            int rowsUpdated = await _Context.SaveChangesAsync();
            return rowsUpdated > 0;
        }
    }
}
