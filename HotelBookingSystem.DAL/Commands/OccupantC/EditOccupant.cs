using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.OccupantC
{
    public record EditOccupant(OccupantDetail Occupant) : IRequest<OccupantDetail>;
}
