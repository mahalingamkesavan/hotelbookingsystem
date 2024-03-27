using System.Text;
using System.Text.Json;

namespace HotelBookingSystem.DAL.Utils
{
    internal static class EncodeDecode
    {
        public static string Encode(this string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }
        public static string Decode(string base64)
        {
            var base64Bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(base64Bytes);
        }
        public static string Encode<T>(this T text)
        {
            var textBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(text));
            return Convert.ToBase64String(textBytes);
        }
        public static T Decode<T>(string base64)
        {
            var base64Bytes = Convert.FromBase64String(base64);
            string JsonString = Encoding.UTF8.GetString(base64Bytes);
            return JsonSerializer.Deserialize<T>(JsonString)!;
        }
    }
}
