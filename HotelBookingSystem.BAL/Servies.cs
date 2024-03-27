using HotelBookingSystem.BAL.Absractes;
using HotelBookingSystem.BAL.Concrets;
//using HotelBookingSystem.BAL.FileHandling;
using HotelBookingSystem.Dal;
using HotelBookingSystem.ImageKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBookingSystem.BAL
{
    public static class ServiesBAL
    {
        public static IServiceCollection AddBAL(this IServiceCollection services, IConfiguration config)
        {
            services.AddDAL(config);
            services.AddScoped<IHotel, HotelServies>();
            services.AddScoped<IUser, UserServies>();
            services.AddScoped<IBooking, BookingServices>();
            services.AddScoped<IOccupant, OccupantServies>();
            services.AddScoped<IRoom, Roomservices>();
            return services;
        }
    }
}