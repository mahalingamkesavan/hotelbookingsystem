using HotelBookingSystem.Authendication;
using HotelBookingSystem.BAL;
using HotelBookingSystem.Models.ResponseModels;
using HotelBookingSystem.Utils;
using HotelBookingSystem.Utils.Filters;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HotelBookingSystem_2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(opt =>
            {
                //opt.
            })
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddExceptionHandler(x =>
            {
                x.AllowStatusCode404Response = true;
                //x.ExceptionHandlingPath = "";
            });

            //builder.Services.

            builder.Host.ConfigureLogging((context, logging) =>
            {
                logging.AddFile(builder.Configuration.GetSection("FileLogging"));
                logging.AddFile(builder.Configuration.GetSection("ErrorLogging"));
                logging.AddFile(builder.Configuration.GetSection("ActionFileLogging"));
            });

            builder.Services.AddHttpLogging(opt =>
            {
                opt.LoggingFields = HttpLoggingFields.Request | HttpLoggingFields.Response;
            });

            builder.Services.AddScoped<LogActionFilter>();

            builder.Services.AddSwaggerGen();

            builder.Services.AddJWTAuth(builder.Configuration);

            builder.Services.AddBAL(builder.Configuration);

            builder.Services.AddCors(x =>
            {
                x.AddDefaultPolicy(x =>
                {
                    x.WithOrigins("http://localhost:3000",
                        "http://localhost:3006",
                        "http://192.168.96.26:3000",
                        "https://hotelbooking-9e82a.firebaseapp.com");
                    x.AllowAnyHeader();
                    x.AllowAnyMethod();
                });
            });

            builder.Services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseDefaultFiles();

            //app.UseUtilsFileServer(app.Configuration);

            app.UseHttpLogging();

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/", () =>
            {
                var res = new BaseResponse<object>
                {
                    Message = "Hotel Booking System 1.0.0",
                };

                return JsonSerializer.Serialize(res,new JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
            });

            app.UseExceptionHandler(AppError =>
            {
                AppError.Run(async x =>
                {
                    var contextFeature = x.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        x.Response.Clear();
                        x.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        x.Response.ContentType = "application/json";
                        x.Response.Headers.ContentType = "application/json";
                        await x.Response.WriteAsJsonAsync(new BaseResponse<object>
                        {
                            StatusCode = StatusCodes.Status500InternalServerError,
                            Message = contextFeature.Error.Message,
                            Error = true
                        }, new JsonSerializerOptions
                        {
                            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                        });
                        await x.Response.CompleteAsync();
                    }
                });
            });

            app.Run();
        }
    }
}