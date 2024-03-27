using HotelBookingSystem.Models.Entities;

namespace HotelBookingSystem.Authendication
{
    public interface IAuthendicate
    {
        string Key { get; }
        string GenerateToken(User user);
    }
}
