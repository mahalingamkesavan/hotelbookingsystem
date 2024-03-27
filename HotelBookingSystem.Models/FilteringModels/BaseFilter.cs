namespace HotelBookingSystem.Models.FilteringModels
{
    public class BaseFilter : Pagination
    {
        public string? Search { get; set; }
        public List<string> OrderBy { get; set; } = new List<string>();
        public bool IsAscending { get; set; } = true;
    }
}
