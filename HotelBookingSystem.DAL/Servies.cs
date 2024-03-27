using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.ImageKit;
using HotelBookingSystem.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBookingSystem.Dal
{
    public static class ServiesDAL
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration config)
        {
            //services.AddFileServer(config);
            services.AddOnlineFileServer(config);
            services.AddDbContext<HotelBookingContext>(x =>
            {
                x.UseSqlServer(config.GetConnectionString("Constr"));
            });
            services.AddMediatR(typeof(ServiesDAL).Assembly);

            return services;
        }

    }
}
