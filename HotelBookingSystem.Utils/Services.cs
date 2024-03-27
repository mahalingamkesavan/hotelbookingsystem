using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace HotelBookingSystem.Utils
{
    public static class Services
    {
        public static IServiceCollection AddFileServer(this IServiceCollection services, IConfiguration configuration)
        {
            string directoryPath = configuration["DirectorySettings:DirectoryPath"] ?? throw new Exception("No Directory is Given!");
            var provider = new PhysicalFileProvider(directoryPath);
            services.AddSingleton<IFileProvider>(provider);
            return services;
        }
        public static IApplicationBuilder UseUtilsFileServer(this IApplicationBuilder app, IConfiguration configuration)
        {
            string directoryPath = configuration["DirectorySettings:DirectoryPath"] ?? throw new Exception("No Directory is Given!"); ;
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(directoryPath),
                //FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory() + "/FileServer"),
                //RequestPath = Directory.GetCurrentDirectory() + "/FileServer",
            });
            return app;
        }
    }
}
