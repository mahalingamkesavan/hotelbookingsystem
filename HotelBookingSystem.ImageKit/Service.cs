using HotelBookingSystem.ImageKit.Concrets;
using HotelBookingSystem.ImageKit.Interfaces;
using Imagekit;
using Imagekit.Sdk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace HotelBookingSystem.ImageKit
{
    public static class Service
    {
        public static IServiceCollection AddOnlineFileServer (this IServiceCollection service, IConfiguration config)
        {
            service.AddScoped<IFileProcessing, FileProcessing>();
            return service;
        }
    }
}