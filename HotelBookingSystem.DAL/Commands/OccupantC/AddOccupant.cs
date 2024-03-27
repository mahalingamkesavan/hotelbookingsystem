using HotelBookingSystem.Models.Entities;
using MediatR;

namespace HotelBookingSystem.DAL.Commands.OccupantC
{
    public record AddOccupant(OccupantDetail Occupant) : IRequest<OccupantDetail>;
}
