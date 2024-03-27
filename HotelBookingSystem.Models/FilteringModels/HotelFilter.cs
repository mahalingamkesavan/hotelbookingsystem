namespace HotelBookingSystem.Models.FilteringModels
{
    public class HotelFilter : BaseFilter
    {
        public string? HotelName { get; set; }
        public string? HotelLocation { get; set; }
        public string? HotelPincode { get; set; }
        public List<int> Ratings { get; set; } = new List<int>() {0, 1, 2, 3, 4, 5 };
        public int MinHotelRate { get; set; } = 0;
        public int MaxHotelRate { get; set; } = int.MaxValue;
    }
}
