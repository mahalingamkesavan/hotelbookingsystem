using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.AminityC
{
    public class DeleteAminity : IRequest<bool>
    {
        public Aminity[]? Aminity { get; set; } = null;
        public int AminityId { get; set; } = -1;
        public DeleteAminity(params Aminity[] aminity)
        {
            Aminity = aminity;
        }
        public DeleteAminity(int aminityId)
        {
            AminityId = aminityId;
        }
    }

    public class DeleteAminityHandler : IRequestHandler<DeleteAminity, bool>
    {

        private readonly HotelBookingContext _Context;

        public DeleteAminityHandler(HotelBookingContext context)
        {
            _Context = context;
        }
        public async Task<bool> Handle(DeleteAminity request, CancellationToken cancellationToken)
        {
            int rowsUpdated = -1;
            if (request.Aminity!=null)
                _Context.Aminities.RemoveRange(request.Aminity);
            Aminity? aminityData = null;
            if (request.AminityId != -1)
            {
                aminityData = _Context.Aminities.SingleOrDefault(x=>x.Id == request.AminityId); 
                _Context.Aminities.Remove(aminityData ?? throw new Exception("Id Not found"));
            }
                
            rowsUpdated = await _Context.SaveChangesAsync();
            return rowsUpdated > 0;
        }
    }
}
