using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.DAL.Commands.AminityC
{
    public class DeleteHotelAminity : IRequest<bool>
    {
        public Aminity[]? Aminity { get; set; } = null;
        public int AminityId { get; set; } = -1;
        public DeleteHotelAminity(params Aminity[] aminity)
        {
            Aminity = aminity;
        }
        public DeleteHotelAminity(int aminityId)
        {
            AminityId = aminityId;
        }
    }

    public class DeleteHotelAminityHandler : IRequestHandler<DeleteHotelAminity, bool>
    {

        private readonly HotelBookingContext _Context;

        public DeleteHotelAminityHandler(HotelBookingContext context)
        {
            _Context = context;
        }
        public async Task<bool> Handle(DeleteHotelAminity request, CancellationToken cancellationToken)
        {
            int rowsUpdated = -1;
            if (request.Aminity != null)
                _Context.Aminities.RemoveRange(request.Aminity);
            Aminity? aminityData = null;
            if (request.AminityId != -1)
            {
                aminityData = _Context.Aminities.SingleOrDefault(x => x.Id == request.AminityId);
                _Context.Aminities.Remove(aminityData ?? throw new Exception("Id Not found"));
            }

            rowsUpdated = await _Context.SaveChangesAsync();
            return rowsUpdated > 0;
        }
    }
}
