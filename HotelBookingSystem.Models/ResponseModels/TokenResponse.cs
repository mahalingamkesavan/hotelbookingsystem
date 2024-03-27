namespace HotelBookingSystem.Models.ResponseModels
{
    public class TokenResponse
    {
        public string Token { get; set; } = null!;
        public DateTime ExpIn { get; set; }
    }
}
